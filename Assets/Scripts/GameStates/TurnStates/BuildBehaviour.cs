using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BuildBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject transactionPanel;
    [SerializeField] Button stopBuildBtn;
    [SerializeField] GameObject buildableEstablishementBtnPrefab;
    [SerializeField] GameObject buildableMonumentBtnPrefab;
    [SerializeField] Transform buildableEstablishementpawnPoint;
    [SerializeField] Transform buildableMonumentSpawnPoint;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        InitButtons();
    }

    void InitButtons() {
        stopBuildBtn.onClick.AddListener(QuitState);
    }

    public void Start() {
        transactionPanel.SetActive(true);
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        transactionPanel.SetActive(false);
        turnState.PerformTurn();
    }

    void StartBuild() {
        /*for(int i=0; i<gameData.cards.Count; i++) {
            Button cardBtn = MonoBehaviour.Instantiate(buildableEstablishementBtnPrefab, buildableEstablishementpawnPoint).GetComponent<Button>();
            cardBtn.interactable = CanBuild(gameData.cards[i]);
            cardBtn.OnCLick.AddListener(() => {BuildCard(gameData.cards[i])});
        }
        
        */
    }

    void BuildCard(Card card) {
        EndBuild();
    }

    bool CanBuild(Card card) {
        return gameData.players[playerTurn].coins >= card.constructionCost;
    }

    void EndBuild() {
        
    }
}
