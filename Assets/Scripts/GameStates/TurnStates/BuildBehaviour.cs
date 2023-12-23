using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

// Serializable class implementing the build behavior for a turn state
[System.Serializable]
public class BuildBehaviour : ITurnState {
    bool isBtnInit = false;
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject transactionPanel; // Panel for transaction during building phase
    [SerializeField] Button stopBuildBtn; // Button to stop the build phase
    [SerializeField] CardUIData cardPrefab; // Prefab for card UI representation
    [SerializeField] CardUIData buildableMonumentBtnPrefab; // Prefab for buildable monument UI representation
    [SerializeField] Transform buildableEstablishmentSpawnPoint; // Spawn point for buildable establishments
    [SerializeField] Transform buildableMonumentSpawnPoint; // Spawn point for buildable monuments
    [SerializeField] Image upSizedCard; // Image display for an upsized card

    CardUIPrefab[] cardPrefabs; // Array to store UI representations of buildable cards

    // Method to initialize the state with necessary data
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
        transactionPanel.SetActive(false);

        InitButtons();
        Start();
    }

    // Method to initialize buttons' click listeners
    void InitButtons() {
        if (isBtnInit) return;
        
        stopBuildBtn.onClick.AddListener(EndBuild);

        isBtnInit = true;
    }

    // Method invoked at the start of the build phase
    public void Start() {
        transactionPanel.SetActive(true);
        StartBuild();

        gameData.players[playerTurn].OptionalPlayerBuild(this, gameData);
    }

    // Method for update operations during the build phase
    public void Update(float dt) {
        // Perform necessary update operations if any
    }

    // Method to clean up and end the build phase
    public void QuitState() {
        transactionPanel.SetActive(false);
        turnState.FinishTurn();
    }

    // Method to start building by initializing buildable cards
    void StartBuild() {
        Establishment[] et = gameData.establishments.Keys.ToArray();
        cardPrefabs = new CardUIPrefab[et.Length + gameData.monuments.Length];

        // Loop to create UI representations for buildable establishments
        for (int i = 0; i < et.Length; i++) {
            int j = i;
            cardPrefabs[j] = new CardUIPrefab(cardPrefab, buildableEstablishmentSpawnPoint, et[i], turnState.game, this);
            cardPrefabs[j].loadedBtn.interactable = CanBuild(et[i]);

            // Logic to check if an establishment can be built and handle button click events
            Establishment establishmentToBuild = gameData.establishments.Keys.ToArray()[j];
            if (gameData.establishments[et[i]] <= 0 || establishmentToBuild.cardType == CardType.CITYLIFE && gameData.players[playerTurn].ContainCardName(establishmentToBuild))
                cardPrefabs[j].loadedBtn.interactable = false;
            else
                cardPrefabs[j].loadedBtn.onClick.AddListener(() => { BuildEstablishmentCard(establishmentToBuild); });
        }

        // Loop to create UI representations for buildable monuments
        for (int i = 0; i < gameData.monuments.Length; i++) {
            int j = i;
            cardPrefabs[et.Length + j] = new CardUIPrefab(cardPrefab, buildableMonumentSpawnPoint, gameData.monuments[i], turnState.game, this);
            cardPrefabs[et.Length + j].loadedBtn.interactable = CanBuild(gameData.monuments[i]);

            // Logic to check if a monument can be built and handle button click events
            if (gameData.players[playerTurn].monumentCards[i].built)
                cardPrefabs[et.Length + j].loadedBtn.interactable = false;
            else
                cardPrefabs[et.Length + j].loadedBtn.onClick.AddListener(() => { BuildMonumentCard(gameData.players[playerTurn].monumentCards[j]); });
        }
    }

    // Method to build an establishment card
    public void BuildEstablishmentCard(Establishment card) {
        gameData.establishments[card]--;
        gameData.players[playerTurn].BuildCardForPlayer(card);
        gameData.players[playerTurn].AddCoin(-card.constructionCost);
        EndBuild();
    }

    // Method to build a monument card
    public void BuildMonumentCard(Monument card) {
        card.built = false;
        gameData.players[playerTurn].BuildMonument(card);
        card.PerformSpecial(gameData.players[playerTurn], gameData.players[playerTurn], gameData.players);
        gameData.players[playerTurn].AddCoin(-card.constructionCost);
        EndBuild();
    }

    // Method to check if a card can be built based on cost
    bool CanBuild(Card card) {
        return gameData.players[playerTurn].coins >= card.constructionCost;
    }

    // Method to clean up UI and end the build phase
    void Dispose() {
        DisplayUpSizedCard(false);
        for (int i = 0; i < cardPrefabs.Length; i++) {
            cardPrefabs[i].Destroy();
        }
    }

    // Method to end the build phase
    public void EndBuild() {
        gameData.players[playerTurn].playerFrame.UpdateUI();
        Dispose();

        QuitState();
    }

    // Method to display an upsized card image
    public void DisplayUpSizedCard(bool value, Sprite imageCard = null) {
        if (value)
            upSizedCard.sprite = imageCard;

        upSizedCard.gameObject.SetActive(value);
    }
}
