using UnityEngine;

[System.Serializable]
public class TransactionBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject transactionPanel;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        Start();
        QuitState();
    }

    public void Start() {
        transactionPanel.SetActive(true);
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        transactionPanel.SetActive(false);
        turnState.Build();
    }

    void Transactions() {
        QuitState();
    }
}
