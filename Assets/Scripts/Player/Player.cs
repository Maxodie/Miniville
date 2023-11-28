using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public string playerName;
    public int coins;
    public int currentDice; 
    public int maxDice; 
    public int totalThrowValue;
    public int[] throwValue;
    public bool hasBuild;
    public bool canReplay = false;
    public List<Establishment> buildingCards = new List<Establishment>();
    List<List<Card>> cardSpawned = new List<List<Card>>();
    public Monument[] monumentCards = new Monument[4];
    public GameObject playerCanvas;
    public GameObject playerBoard;
    public bool isRealPlayer {get; private set;}
    
    public Player(bool isRealPlayer, string playerName, int coins, int maxDices, int currentDice, List<Establishment> deck, Monument[] monument, GameObject playerCanvas) //add
    {
        this.isRealPlayer = isRealPlayer;
        this.playerName = playerName;
        this.coins = coins;
        this.maxDice = maxDices; 
        this.currentDice = currentDice;
        this.buildingCards = deck;
        this.monumentCards = monument;
        this.playerCanvas = playerCanvas;
    }

    public void Start(GameObject playerBoard) {
        this.playerBoard = playerBoard;
        for(int i=0; i < buildingCards.Count; i++) {
            BuildCardForPlayer(buildingCards[i]);
        }

        SpawnMonumentForPlayer();
    }
    
    public void AddCard(Establishment establishment) //The method add a card to the player deck
    {
        buildingCards.Add(establishment);
    }

    public void BuildMonument(Monument monument) {
        for(int i=0; i<monumentCards.Length; i++) {
            if(monumentCards[i] == monument) {
                monumentCards[i].built = true;
                monumentCards[i].InstantiateBuilding(playerBoard.transform.position + new Vector3(-10f, 0f, 1f * i) * playerBoard.transform.forward.z, playerBoard.transform.rotation);
            }
        }
    }

    public bool GetMonumentBuiltByType(Type monumentType) {
        for(int i=0; i < monumentCards.Length; i++) {
            if(monumentCards[i].GetType() == monumentType) {
                return monumentCards[i].built;
            }
        }

        return false;
    }
    
    public int GetCardsNbByType(CardType cardType) //Since some cards have effects depending on what number of that precise type of card you have, we need a function that count a specific type of card
    {
        int count = 0;
        
        foreach (var establishment in buildingCards) //We compare all the card type that the player have with the card type we want to check
        {
            if (establishment.cardType == cardType)
            {
                count++;
            }
        }

        return count;
    }
    
    public void ThrowDice(int[] diceChoice) //We perform a throw depending on how much dices the player want to throw
    {
        totalThrowValue = 0;
        throwValue = new int[diceChoice.Length];
        for(int i=0; i < diceChoice.Length; i++) {
            throwValue[i] = diceChoice[i];
            totalThrowValue += throwValue[i];
        }
    }

    public void BuildCardForPlayer(Card cardToBuild) {
        Vector3 pos = Vector3.zero;
        bool spawnBuilding = true;
        bool doAnotherColomn = true;

        if(cardSpawned.Count > 0)
            pos = new Vector3(0f, 0f, cardSpawned[cardSpawned.Count - 1][cardSpawned[cardSpawned.Count - 1].Count - 1].spawnedGoCard.transform.position.z + 3f);

        for(int i=0; i< cardSpawned.Count; i++) {
            if(cardSpawned[i][0].cardName == cardToBuild.cardName) {
                pos = new Vector3(0f, 0f, cardSpawned[i][cardSpawned[i].Count - 1].spawnedGoCard.transform.position.z + 0.5f);
                spawnBuilding = false;
                doAnotherColomn = false;

                cardSpawned[i].Add(cardToBuild);
            }
        }

        if(doAnotherColomn) {
            cardSpawned.Add(new List<Card>());
            cardSpawned[cardSpawned.Count - 1].Add(cardToBuild);
        }

        

        cardToBuild.InstantiateCard(playerBoard.transform.position + pos, playerBoard.transform.rotation, spawnBuilding);
    }

    void SpawnMonumentForPlayer() {
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i].InstantiateCard(playerBoard.transform.position + new Vector3(-10f, 0f, 1f * i) * playerBoard.transform.forward.z, playerBoard.transform.rotation, false);
        }
    }

    public virtual void OptionalPlayerThrowDice(ITurnState turnState) { }
    public virtual void OptionalPlayerBuild(ITurnState turnState) { }
    public virtual void OptionalPlayerInteraction(ITurnState turnState) { }
}
