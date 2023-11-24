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
    [SerializeField] PlayerDice playerDice;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        InitButtons();

        Start();
    }

    public void Start() {
        playerDicePanel.SetActive(true);

        throwOneDice.interactable = true;
        if(gameData.players[playerTurn].currentDice > 1) 
            throwTwoDice.interactable = true;
        else
            throwTwoDice.interactable = false;

        gameData.players[playerTurn].OptionalPlayerThrowDice(this);
    }
    
    void InitButtons() {
        if(isBtnInit) return;

        throwOneDice.onClick.AddListener(PlayerThrowOneDice);
        throwTwoDice.onClick.AddListener(PlayerThrowTwoDice);

        isBtnInit = true;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        playerDicePanel.SetActive(false);
        turnState.Transactions();
    }

    public void PlayerThrowOneDice() {
        playerDice.ThrowDice(1, gameData.players[playerTurn].ThrowDice, QuitState);
        DisableBtn();
    }

    public void PlayerThrowTwoDice() {
        playerDice.ThrowDice(2, gameData.players[playerTurn].ThrowDice, QuitState);
        DisableBtn();
    }

    void DisableBtn() {
        throwOneDice.interactable = false;
        throwTwoDice.interactable = false;
    }
}
