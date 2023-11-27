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
    List<Card> cardSpawned = new List<Card>();
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
    
    public void AddCard(Establishment establishment) //The method add a card to the player deck
    {
        buildingCards.Add(establishment);
    }

    public void BuildMonument(Monument monument) {
        for(int i=0; i<monumentCards.Length; i++) {
            if(monumentCards[i] == monument)
                monumentCards[i].built = true;
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

        if(cardSpawned.Count > 0)
            pos = cardSpawned[cardSpawned.Count - 1].spawnedGoCard.transform.position + new Vector3(0f, 0f, 2f);

        for(int i=0; i< cardSpawned.Count; i++) {
            if(cardSpawned[i].cardName == cardToBuild.cardName) {
                pos = cardSpawned[i].spawnedGoCard.transform.position + new Vector3(0f, 0f, 0.1f);
                spawnBuilding = false;
            }
        }

        cardToBuild.InstantiateCard(playerBoard.transform.position + pos, playerBoard.transform.rotation, spawnBuilding);
        cardSpawned.Add(cardToBuild);
    }

    public virtual void OptionalPlayerThrowDice(ITurnState turnState) { }
    public virtual void OptionalPlayerBuild(ITurnState turnState) { }
    public virtual void OptionalPlayerInteraction(ITurnState turnState) { }
}
