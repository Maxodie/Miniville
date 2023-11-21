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
    [SerializeField] GameObject buildableEstablishmentBtnPrefab;
    [SerializeField] GameObject buildableMonumentBtnPrefab;
    [SerializeField] Transform buildableEstablishmentSpawnPoint;
    [SerializeField] Transform buildableMonumentSpawnPoint;

    CardPrefab[] cardPrefabs;

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
        
        stopBuildBtn.onClick.AddListener(QuitState);

        isBtnInit = true;
    }

    public void Start() {
        transactionPanel.SetActive(true);
        StartBuild();
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        transactionPanel.SetActive(false);
        turnState.PerformTurn();
    }

    void StartBuild() {
        Establishment[] et = gameData.establishments.Keys.ToArray();
        cardPrefabs = new CardPrefab[et.Length + gameData.monuments.Length];

        for(int i=0; i<gameData.establishments.Count; i++) {
            int j = i;
            Button cardBtn = Object.Instantiate(buildableEstablishmentBtnPrefab, buildableEstablishmentSpawnPoint).GetComponent<Button>();
            cardBtn.interactable = CanBuild(et[i]);

            if(gameData.establishments[et[i]] <= 0)
                cardBtn.interactable  = false;

            cardBtn.onClick.AddListener(delegate {BuildEstablishmentCard(gameData.establishments.Keys.ToArray()[j]);});
            
            //init card prefab
            cardPrefabs[j] = new CardPrefab(et[i], cardBtn.gameObject);
        }

        for(int i=0; i<gameData.monuments.Length; i++) {
            int j = i;
            Button cardBtn = Object.Instantiate(buildableMonumentBtnPrefab, buildableMonumentSpawnPoint).GetComponent<Button>();
            cardBtn.interactable = CanBuild(gameData.monuments[i]);

            if(gameData.players[playerTurn].monumentCards[i].built)
                cardBtn.interactable = false;

            cardBtn.onClick.AddListener(() => {BuilddMonumentCard(gameData.players[playerTurn].monumentCards[j]);});
            
            //init card prefab
            cardPrefabs[et.Length + j] = new CardPrefab(et[i], cardBtn.gameObject);
        }
    }

    void BuildEstablishmentCard(Establishment card) {
        gameData.establishments[card]--;
        gameData.players[playerTurn].AddCard(card);
        EndBuild();
    }

    void BuilddMonumentCard(Monument card) {
        card.built = false;
        gameData.players[playerTurn].BuildMonument(card);
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
