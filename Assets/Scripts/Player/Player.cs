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
    public PlayerFrame playerFrame;

    //player board offsets
    int cardNewLine = 5;
    int newLineXOffset = 4;
    float buildingsZOffsetInRaw = 3;
    float cardInPackOffset = 0.3f;
    int monumentSpace = -10;
    int currentBuildingXPos = 0;
    float monumentOffset = 2f;
    float zBoradOffset;
    
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
        zBoradOffset = (cardNewLine-1) * buildingsZOffsetInRaw / 2;
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
                monumentCards[i].InstantiateBuilding(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset));
                Debug.Log("te");
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
        Vector3 pos = new Vector3(0f, 0f, -zBoradOffset);
        bool spawnBuilding = true;
        bool doAnotherColomn = true;

        if(cardSpawned.Count > 0) {
            if(cardSpawned.Count % cardNewLine == 0) {
                currentBuildingXPos = cardSpawned.Count / cardNewLine;
            }
            
           pos = new Vector3(currentBuildingXPos * newLineXOffset, 0f, cardSpawned.Count % cardNewLine == 0 ? -zBoradOffset : cardSpawned[cardSpawned.Count - (cardNewLine * currentBuildingXPos) - 1][0].spawnedGoBuilding.transform.localPosition.z + buildingsZOffsetInRaw); //set the position of the buildings in line
        }

        for(int i=0; i< cardSpawned.Count; i++) {
            if(cardSpawned[i][0].cardName == cardToBuild.cardName) {
                pos = new Vector3(0f, 0f, cardSpawned[i][cardSpawned[i].Count - 1].spawnedGoCard.transform.localPosition.z + cardInPackOffset);
                spawnBuilding = false;
                doAnotherColomn = false;

                cardSpawned[i].Add(cardToBuild);
            }
        }

        if(doAnotherColomn) {
            cardSpawned.Add(new List<Card>());
            cardSpawned[cardSpawned.Count - 1].Add(cardToBuild);
        }

        cardToBuild.InstantiateCard(playerBoard.transform, pos, spawnBuilding);
    }

    void SpawnMonumentForPlayer() {
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i].InstantiateCard(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset), false);
        }
    }

    public virtual void OptionalPlayerThrowDice(ITurnState turnState) { }
    public virtual void OptionalPlayerBuild(ITurnState turnState) { }
    public virtual void OptionalPlayerInteraction(ITurnState turnState) { }
}
