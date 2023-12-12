using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuildBehaviour : ITurnState {
    bool isBtnInit = false;
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject transactionPanel;
    [SerializeField] Button stopBuildBtn;
    [SerializeField] CardUIData cardPrefab;
    [SerializeField] CardUIData buildableMonumentBtnPrefab;
    [SerializeField] Transform buildableEstablishmentSpawnPoint;
    [SerializeField] Transform buildableMonumentSpawnPoint;

    CardUIPrefab[] cardPrefabs;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
        transactionPanel.SetActive(false);

        InitButtons();
        Start();
    }

    void InitButtons() {
        if(isBtnInit) return;
        
        stopBuildBtn.onClick.AddListener(EndBuild);

        isBtnInit = true;
    }

    public void Start() {
        transactionPanel.SetActive(true);
        StartBuild();

        gameData.players[playerTurn].OptionalPlayerBuild(this);
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        transactionPanel.SetActive(false);
        turnState.PerformTurn();
    }

    void StartBuild() {
        Establishment[] et = gameData.establishments.Keys.ToArray();
        cardPrefabs = new CardUIPrefab[et.Length + gameData.monuments.Length];

        for(int i=0; i<et.Length; i++) {
            int j = i;
            cardPrefabs[j] = new CardUIPrefab(cardPrefab, buildableEstablishmentSpawnPoint, et[i]);
            cardPrefabs[j].loadedBtn.interactable = CanBuild(et[i]);

            Establishment establishmentToBuild = gameData.establishments.Keys.ToArray()[j];

            if(gameData.establishments[et[i]] <= 0 || establishmentToBuild.cardType == CardType.CITYLIFE && gameData.players[playerTurn].ContainCardName(establishmentToBuild))
                cardPrefabs[j].loadedBtn.interactable  = false;
            else
                cardPrefabs[j].loadedBtn.onClick.AddListener(delegate {BuildEstablishmentCard(establishmentToBuild);});
        }

        for(int i=0; i<gameData.monuments.Length; i++) {
            int j = i;
            cardPrefabs[et.Length + j] = new CardUIPrefab(cardPrefab, buildableMonumentSpawnPoint, gameData.monuments[i]);
            cardPrefabs[et.Length + j].loadedBtn.interactable = CanBuild(gameData.monuments[i]);

            if(gameData.players[playerTurn].monumentCards[i].built)
                cardPrefabs[et.Length + j].loadedBtn.interactable = false;
            else
                cardPrefabs[et.Length + j].loadedBtn.onClick.AddListener(() => {BuilddMonumentCard(gameData.players[playerTurn].monumentCards[j]);});
        }
    }

    void BuildEstablishmentCard(Establishment card) {
        gameData.establishments[card]--;
        gameData.players[playerTurn].BuildCardForPlayer(card);
        gameData.players[playerTurn].AddCoin(-card.constructionCost);
        EndBuild();
    }

    void BuilddMonumentCard(Monument card) {
        card.built = false;
        
        gameData.players[playerTurn].BuildMonument(card);
        card.PerformSpecial(gameData.players[playerTurn], gameData.players[playerTurn], gameData.players);
        gameData.players[playerTurn].AddCoin(-card.constructionCost);
        EndBuild();
    }

    bool CanBuild(Card card) {
        return gameData.players[playerTurn].coins >= card.constructionCost;
    }

    void EndBuild() {
        turnState.UpdateCoinText();

        for (int i = 0; i < cardPrefabs.Length; i++)
        {
            cardPrefabs[i].Destroy();
        }

        QuitState();
    }
}
