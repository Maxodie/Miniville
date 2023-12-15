using UnityEngine.UI;
using UnityEngine;

// Serializable class implementing behavior for throwing dice during a turn state
[System.Serializable]
public class ThrowDiceBehaviour : ITurnState {
    bool isBtnInit = false;
    bool isThrowDiceRestarted = false;
    GameData gameData;
    int playerTurn;
    TurnState turnState;
    UIData uiData;
    [SerializeField] Button throwOneDice;
    [SerializeField] Button throwTwoDice;

    [SerializeField] GameObject playerDicePanel;
    [SerializeField] PlayerDice playerDice;

    [Header("Restart ThrowDice")]
    [SerializeField] GameObject restartPanel;
    [SerializeField] Button yesRestartBtn;
    [SerializeField] Button noRestartBtn;

    // Method to initialize the state with necessary data
    public void InitState(GameData gameData, int playerTurn, TurnState turnState, UIData uiData) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
        this.uiData = uiData;

        restartPanel.SetActive(false);

        InitButtons();

        Start();
    }

    // Method invoked at the start of the throw dice phase
    public void Start() {
        playerDicePanel.SetActive(true);

        throwOneDice.interactable = true;
        if (gameData.players[playerTurn].currentDice > 1) {
            throwTwoDice.interactable = true;
            throwTwoDice.GetComponent<Image>().sprite = uiData.blueBtnSprite;
        } else {
            throwTwoDice.interactable = false;
            throwTwoDice.GetComponent<Image>().sprite = uiData.grayBtnSprite;
        }

        gameData.players[playerTurn].OptionalPlayerThrowDice(this, gameData);
    }
    
    // Method to initialize buttons' click listeners
    void InitButtons() {
        if (isBtnInit) return;

        throwOneDice.onClick.AddListener(PlayerThrowOneDice);
        throwTwoDice.onClick.AddListener(PlayerThrowTwoDice);

        yesRestartBtn.onClick.AddListener(YesRestartThrowDice);
        noRestartBtn.onClick.AddListener(QuitState);

        isBtnInit = true;
    }

    // Method for update operations during the throw dice phase
    public void Update(float dt) {
        // Perform necessary update operations if any
    }

    // Method to exit the throw dice state
    public void QuitState() {
        playerDicePanel.SetActive(false);
        
        isThrowDiceRestarted = false;
        turnState.Transactions();
    }

    // Method to handle the end of dice throwing
    void EndThrow() {
        if (!isThrowDiceRestarted && gameData.players[playerTurn].GetMonumentBuiltByType(typeof(RadioTower))) {
            restartPanel.SetActive(true);
        } else {
            QuitState();
        }
    }

    // Method to restart the throw dice phase
    void YesRestartThrowDice() {
        playerDicePanel.SetActive(false);

        isThrowDiceRestarted = true;
        turnState.ThrowDice();
    }

    // Method for a player to throw one dice
    public void PlayerThrowOneDice() {
        playerDice.ThrowDice(1, gameData.players[playerTurn].ThrowDice, EndThrow);
        DisableBtn();
    }

    // Method for a player to throw two dice
    public void PlayerThrowTwoDice() {
        playerDice.ThrowDice(2, gameData.players[playerTurn].ThrowDice, EndThrow);
        DisableBtn();
    }

    // Method to disable dice throw buttons
    void DisableBtn() {
        throwOneDice.interactable = false;
        throwTwoDice.interactable = false;
    }
}
