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
    public List<List<Establishment>> buildingCards = new List<List<Establishment>>();
    public Monument[] monumentCards = new Monument[4];
    public GameObject playerCanvas;
    public GameObject playerBoard;
    public bool isRealPlayer {get; private set;}

    List<Establishment> startDeck;

    //player board offsets
    int cardNewLine = 5;
    int newLineXOffset = 4;
    float buildingsZOffsetInRaw = 3;
    float cardInPackOffset = 0.3f;
    int monumentSpace = -10;
    int currentBuildingXPos = 0;
    float monumentOffset = 2f;
    float zBoradOffset;
    
    public Player(bool isRealPlayer, string playerName, int coins, int maxDices, int currentDice, List<Establishment> startDeck, Monument[] monument, GameObject playerCanvas) //add
    {
        this.isRealPlayer = isRealPlayer;
        this.playerName = playerName;
        this.coins = coins;
        this.maxDice = maxDices; 
        this.currentDice = currentDice;
        this.monumentCards = monument;
        this.playerCanvas = playerCanvas;
        this.startDeck = startDeck;

        InitMonuments();
    }

    public void Start(GameObject playerBoard) {
        this.playerBoard = playerBoard;
        zBoradOffset = (cardNewLine-1) * buildingsZOffsetInRaw / 2;
        InitStartDeck();
        SpawnMonumentForPlayer();
    }

    void InitMonuments() {
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i].CreateNewCardBehaviour();
        }
    }

    void InitStartDeck() {
        for(int i=0; i < startDeck.Count; i++) {
            BuildCardForPlayer(startDeck[i]);
        }
    }

    public void BuildMonument(Monument monument) {
        for(int i=0; i<monumentCards.Length; i++) {
            if(monumentCards[i] == monument) {
                monumentCards[i].built = true;
                monumentCards[i].cardBehaviour.InstantiateBuilding(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset));
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
            if (establishment[0].cardType == cardType)
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

    public void BuildCardForPlayer(Establishment cardToBuild) {
        Vector3 pos = new Vector3(0f, 0f, -zBoradOffset);
        bool spawnBuilding = true;
        bool doAnotherColomn = true;
        cardToBuild = cardToBuild.Copy();
        cardToBuild.CreateNewCardBehaviour();

        if(buildingCards.Count > 0) {
            if(buildingCards.Count % cardNewLine == 0) {
                currentBuildingXPos = buildingCards.Count / cardNewLine;
            }

            int lastRawBuildingPos = buildingCards.Count - (cardNewLine * currentBuildingXPos) - 1;
           // if(buildingCards[lastRawBuildingPos][0].cardBehaviour != null) {
                
               pos = new Vector3(currentBuildingXPos * newLineXOffset, 0f, buildingCards.Count % cardNewLine == 0 ? -zBoradOffset : buildingCards[lastRawBuildingPos][0].cardBehaviour.spawnedGoBuilding.transform.localPosition.z + buildingsZOffsetInRaw); //set the position of the buildings in line
            /*}
            else {
                cardToBuild.cardBehaviour.InstantiateCard(playerBoard.transform, pos, spawnBuilding);
                return;
            }*/
        }

        for(int i=0; i< buildingCards.Count; i++) {
            if(buildingCards[i][0].cardName == cardToBuild.cardName) {
                pos = new Vector3(0f, 0f, buildingCards[i][buildingCards[i].Count - 1].cardBehaviour.spawnedGoCard.transform.localPosition.z + cardInPackOffset);
                spawnBuilding = false;
                doAnotherColomn = false;

                buildingCards[i].Add(cardToBuild);
            }
        }

        if(doAnotherColomn) {
            buildingCards.Add(new List<Establishment>());
            buildingCards[buildingCards.Count - 1].Add(cardToBuild);
        }

        cardToBuild.cardBehaviour.InstantiateCard(playerBoard.transform, pos, spawnBuilding);
    }

    void SpawnMonumentForPlayer() {
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i].cardBehaviour.InstantiateCard(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset), false);
        }
    }

    public virtual void OptionalPlayerThrowDice(ITurnState turnState) { }
    public virtual void OptionalPlayerBuild(ITurnState turnState) { }
    public virtual void OptionalPlayerInteraction(ITurnState turnState) { }
}
