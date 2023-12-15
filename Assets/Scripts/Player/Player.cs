using System;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    //Variables
    public string playerName;
    public int coins;
    public int currentDice; 
    public int maxDice; 
    public int totalThrowValue;
    public int[] throwValue;
    public bool hasBuild;
    public bool canReplay = false;
    public List<List<Establishment>> buildingCards = new List<List<Establishment>>(); //current player establishments
    public Monument[] monumentCards = new Monument[4]; //player monuments
    public GameObject playerCanvas;
    public GameObject playerBoard;
    public bool isRealPlayer {get; private set;}
    public PlayerFrame playerFrame;

    List<Establishment> startDeck;

    //player board offsets
    int cardNewLine = 5;
    int newLineXOffset = 4;
    float buildingsZOffsetInRaw = 3;
    float cardInPackZOffset = 0.3f;
    float cardInPackXOffset = 0.1f;
    int monumentSpace = -10;
    float monumentOffset = 2f;
    float zBoradOffset;
    float currentBuildingXPos;
    
    //Constructor
    public Player(bool isRealPlayer, string playerName, int coins, int maxDices, int currentDice, List<Establishment> startDeck, Monument[] monument, GameObject playerCanvas, GameObject playerFrame, UIPlayerFrameScriptableObject uIPlayerFrameScriptableObject) //add
    {
        this.isRealPlayer = isRealPlayer;
        this.playerName = playerName;
        this.coins = coins;
        this.maxDice = maxDices; 
        this.currentDice = currentDice;
        this.playerCanvas = playerCanvas;
        this.startDeck = startDeck;

        this.monumentCards = new Monument[monument.Length];
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i] = monument[i].Copy();
            monumentCards[i].CreateNewCardBehaviour();
        }

        //show the playerFrame
        playerFrame.SetActive(true);
        this.playerFrame = new PlayerFrame(this, playerFrame, playerName, uIPlayerFrameScriptableObject);
    
    }

    public void Start(GameObject playerBoard) {
        //Set the playerBoard with his position and init the deck
        this.playerBoard = playerBoard;
        zBoradOffset = (cardNewLine-1) * buildingsZOffsetInRaw / 2;
        InitStartDeck();
        SpawnMonumentForPlayer();
    }
    
    //Add the amount of coin to the player
    public void AddCoin(int amount) {
        coins += amount;
        playerFrame.UpdateUI();
    }

    //instantiate and load default cards in the player
    void InitStartDeck() {
        for(int i=0; i < startDeck.Count; i++) {
            BuildCardForPlayer(startDeck[i]);
        }
    }

    //set the monument buildState to true and instantiate the building on it
    public void BuildMonument(Monument monument) {
        for(int i=0; i<monumentCards.Length; i++) {
            if(monumentCards[i] == monument) {
                monumentCards[i].built = true;
                monumentCards[i].cardBehaviour.InstantiateBuilding(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset - (monumentCards.Length / 3 * monumentOffset) ));
            }
        }

        playerFrame.UpdateUI();
    }

    //Return monument typeof "monumentType" built state
    public bool GetMonumentBuiltByType(Type monumentType) {
        for(int i=0; i < monumentCards.Length; i++) {
            if(monumentCards[i].GetType() == monumentType) {
                return monumentCards[i].built;
            }
        }

        return false;
    }
    
    //Return the number of establishment in the list of the cards named "cardName"
    bool HasCardsOfName(string cardName) //Since some cards have effects depending on what number of that precise type of card you have, we need a function that count a specific type of card
    {   
        foreach (var establishment in buildingCards) //We compare all the card type that the player have with the card type we want to check
        {
            if (establishment[0].cardName == cardName)
            {
                return true;
            }
        }

        return false;
    }

    //Get the list of cards with the name "cardName" with name
    public List<Establishment> GetCardStackByName(string cardName) {
        foreach (var establishment in buildingCards)
        {
            if (establishment[0].cardName == cardName)
            {
                return establishment;
            }
        }

        return null;
    }

    //return the number of monument built
    public int GetMonumentBuilt() {
        int count = 0;
        for(int i=0; i < monumentCards.Length; i++) {
            if(monumentCards[i].built) {
                count ++;
            }
        }

        return count;
    }

    //Return true if the player has the "establishment" in his deck
    public bool ContainCardName(Establishment establishment) {
        for (int i=0; i < buildingCards.Count; i++) {
            if(buildingCards[i][0].cardName == establishment.cardName)
                return true;
        }

        return false;
    }


    //exchange the card with the player card "cardID" and the "otherPlayer" in Id "otherPlayerCardID"
    public void ExchangeCard(int cardId, Player otherPlayer, int otherPlayerCardId) {
        //Store thr card witch willl be exchanged get thanks to the id
        Establishment otherPlayerCard = otherPlayer.buildingCards[otherPlayerCardId][otherPlayer.buildingCards[otherPlayerCardId].Count - 1];
        Establishment playerCard = buildingCards[cardId][buildingCards[cardId].Count - 1];
       
       //if the exchanged cards are the same then skip
        bool isSameCard = otherPlayerCard.cardName == playerCard.cardName;

        if(isSameCard) return; //if the cards are the same just skip
        
        //destroy the building in playerBoard and build the exchanged card (for the two players)
        RemoveBuilding(cardId);
        BuildCardForPlayer(otherPlayerCard);

        otherPlayer.RemoveBuilding(otherPlayerCardId);
        otherPlayer.BuildCardForPlayer(playerCard);
    }

    //Destroy the model and remove the building from the establishmentCards list
    public void RemoveBuilding(int playerCardId) {
        Establishment establishment = buildingCards[playerCardId][buildingCards[playerCardId].Count - 1];
        establishment.DestroyCard();

        buildingCards[playerCardId].Remove(establishment);

        //If there are no moe cards then remove the category from the buildingCards list
        if(buildingCards[playerCardId].Count == 0)
            buildingCards.RemoveAt(playerCardId);

        UpdatePlayerDeckPos();
    }
    
    //set the throw diceValue with a int array (result of all dice)
    public void ThrowDice(int[] diceChoice) //We perform a throw depending on how much dices the player want to throw
    {
        totalThrowValue = 0;
        throwValue = new int[diceChoice.Length];
        for(int i=0; i < diceChoice.Length; i++) {
            throwValue[i] = diceChoice[i];
            totalThrowValue += throwValue[i];
        }
    }

    //build the card and add it to the establishmentList
    public void BuildCardForPlayer(Establishment cardToBuild) {
        bool spawnBuilding;

        //Create a copy of it from the gameData dictionnary to the player
        cardToBuild = cardToBuild.Copy();
        cardToBuild.CreateNewCardBehaviour();

        //if there are no cards like this then create a new stack of it
        if(!HasCardsOfName(cardToBuild.cardName)) {
            buildingCards.Add(new List<Establishment>());
            buildingCards[buildingCards.Count - 1].Add(cardToBuild);
            spawnBuilding = true;
        }
        else {
            GetCardStackByName(cardToBuild.cardName).Add(cardToBuild);
            spawnBuilding = false;
        }

        cardToBuild.cardBehaviour.InstantiateCard(playerBoard.transform, Vector3.zero, spawnBuilding);
        playerFrame.UpdateUI();
        UpdatePlayerDeckPos();
    }

    //draw all cards in the playerBoard
    void UpdatePlayerDeckPos() {
        for(int i=0; i < buildingCards.Count; i++) {
            Vector3 pos = new Vector3(currentBuildingXPos * newLineXOffset, 0f, i % cardNewLine == 0 ? -zBoradOffset : buildingCards[i-1][0].cardBehaviour.spawnedGoBuilding.transform.localPosition.z + buildingsZOffsetInRaw); //set the position of the buildings in front of player

            buildingCards[i][0].cardBehaviour.spawnedGoBuilding.transform.localPosition = pos;
            buildingCards[i][0].cardBehaviour.spawnedGoCard.transform.localPosition = pos;

            for(int j=1; j < buildingCards[i].Count; j++) {
                buildingCards[i][j].cardBehaviour.spawnedGoCard.transform.localPosition = pos + new Vector3(cardInPackXOffset * j, .01f * j, cardInPackZOffset * j);
            }
        }
    }

    //instantiate the building of the Monument
    void SpawnMonumentForPlayer() {
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i].cardBehaviour.InstantiateCard(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset - (monumentCards.Length / 3 * monumentOffset) ), false);
        }
    }

    //variables call in each state for particular work.
    public virtual void OptionalPlayerThrowDice(ThrowDiceBehaviour throwDiceBehaviour, GameData gameData) { }
    public virtual void OptionalPlayerBuild(BuildBehaviour buildBehaviour, GameData gameData) { }
    public virtual void OptionalPlayerInteraction(InteractionBehaviour interactionBehaviour, GameData gameData) { }
}
 