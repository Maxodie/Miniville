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
    
    public Player(bool isRealPlayer, string playerName, int coins, int maxDices, int currentDice, List<Establishment> startDeck, Monument[] monument, GameObject playerCanvas, GameObject playerFrame, UIPlayerFrameScriptableObject uIPlayerFrameScriptableObject) //add
    {
        this.isRealPlayer = isRealPlayer;
        this.playerName = playerName;
        this.coins = coins;
        this.maxDice = maxDices; 
        this.currentDice = currentDice;
        this.monumentCards = monument;
        this.playerCanvas = playerCanvas;
        this.startDeck = startDeck;

        playerFrame.SetActive(true);
        this.playerFrame = new PlayerFrame(this, playerFrame, playerName, uIPlayerFrameScriptableObject);
        
        InitMonuments();
    }

    public void Start(GameObject playerBoard) {
        this.playerBoard = playerBoard;
        zBoradOffset = (cardNewLine-1) * buildingsZOffsetInRaw / 2;
        InitStartDeck();
        SpawnMonumentForPlayer();
    }

    public void AddCoin(int amount) {
        coins += amount;
        playerFrame.UpdateUI();
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

        playerFrame.UpdateUI();
    }

    public bool GetMonumentBuiltByType(Type monumentType) {
        for(int i=0; i < monumentCards.Length; i++) {
            if(monumentCards[i].GetType() == monumentType) {
                return monumentCards[i].built;
            }
        }

        return false;
    }
    
    public int GetCardsNbByName(string cardName) //Since some cards have effects depending on what number of that precise type of card you have, we need a function that count a specific type of card
    {
        int count = 0;
        
        foreach (var establishment in buildingCards) //We compare all the card type that the player have with the card type we want to check
        {
            if (establishment[0].cardName == cardName)
            {
                count++;
            }
        }

        return count;
    }

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

    public int GetMonumentBuilt() {
        int count = 0;
        for(int i=0; i < monumentCards.Length; i++) {
            if(monumentCards[i].built) {
                count ++;
            }
        }

        return count;
    }

    public bool ContainCardName(Establishment establishment) {
        for (int i=0; i < buildingCards.Count; i++) {
            if(buildingCards[i][0].cardName == establishment.cardName)
                return true;
        }

        return false;
    }

    public void ExchangeCard(int cardId, Player otherPlayer, int otherPlayerCardId) {
        Establishment otherPlayerCard = otherPlayer.buildingCards[otherPlayerCardId][otherPlayer.buildingCards[otherPlayerCardId].Count - 1];
        Establishment playerCard = buildingCards[cardId][buildingCards[cardId].Count - 1];
       
        bool isSameCard = otherPlayerCard.cardName == playerCard.cardName;

        if(isSameCard) return; //if the cards are the same just skip
        
        RemoveBuilding(cardId);
        BuildCardForPlayer(otherPlayerCard);

        otherPlayer.RemoveBuilding(otherPlayerCardId);
        otherPlayer.BuildCardForPlayer(playerCard);
    }

    public void RemoveBuilding(int playerCardId) {
        Establishment establishment = buildingCards[playerCardId][buildingCards[playerCardId].Count - 1];
        establishment.DestroyCard();

        buildingCards[playerCardId].Remove(establishment);

        if(buildingCards[playerCardId].Count == 0)
            buildingCards.RemoveAt(playerCardId);

        UpdatePlayerDeckPos();
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
        bool spawnBuilding;

        cardToBuild = cardToBuild.Copy();
        cardToBuild.CreateNewCardBehaviour();

        if(GetCardsNbByName(cardToBuild.cardName) == 0) {
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

    void SpawnMonumentForPlayer() {
        for(int i=0; i < monumentCards.Length; i++) {
            monumentCards[i].cardBehaviour.InstantiateCard(playerBoard.transform, new Vector3(monumentSpace, 0f, i * monumentOffset - (monumentCards.Length / 3 * monumentOffset) ), false);
        }
    }

    public virtual void OptionalPlayerThrowDice(ThrowDiceBehaviour throwDiceBehaviour, GameData gameData) { }
    public virtual void OptionalPlayerBuild(BuildBehaviour buildBehaviour, GameData gameData) { }
    public virtual void OptionalPlayerInteraction(InteractionBehaviour interactionBehaviour, GameData gameData) { }
}
 