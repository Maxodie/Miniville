using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class ThrowDiceBehaviour : ITurnState {
    bool isBtnInit = false;
    GameData gameData;
    int playerTurn;
    TurnState turnState;
    [SerializeField] Button throwOneDice;
    [SerializeField] Button throwTwoDice;

    [SerializeField] GameObject playerDicePanel;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        InitButtons();

        Start();
    }

    public void Start() {
        playerDicePanel.SetActive(true);

        if(gameData.players[playerTurn].maxDice > 1)
            throwTwoDice.interactable = true;
        else
            throwTwoDice.interactable = false;
    }
    
    void InitButtons() {
        if(isBtnInit) return;

        throwOneDice.onClick.AddListener(PlayerThrowOneDice);
        throwTwoDice.onClick.AddListener(PlayerThrowTwoDice);
        Debug.Log("rr");

        isBtnInit = true;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        playerDicePanel.SetActive(false);
        turnState.Transactions();
    }

    public void PlayerThrowOneDice() {
        gameData.players[playerTurn].ThrowDice(1);
        QuitState();
    }

    public void PlayerThrowTwoDice() {
        gameData.players[playerTurn].ThrowDice(2);
        QuitState();
    }
}
