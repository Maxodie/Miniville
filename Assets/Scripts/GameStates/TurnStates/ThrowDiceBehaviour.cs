using UnityEngine.UI;
using UnityEngine;

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

    public void InitState(GameData gameData, int playerTurn, TurnState turnState, UIData uiData) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
        this.uiData = uiData;

        restartPanel.SetActive(false);

        InitButtons();

        Start();
    }

    public void Start() {
        playerDicePanel.SetActive(true);

        throwOneDice.interactable = true;
        if (gameData.players[playerTurn].currentDice > 1)
        {
            throwTwoDice.interactable = true;
            throwTwoDice.GetComponent<Image>().sprite = uiData.blueBtnSprite;
        }
        else
        {
            throwTwoDice.interactable = false;
            throwTwoDice.GetComponent<Image>().sprite = uiData.grayBtnSprite;
        }

        gameData.players[playerTurn].OptionalPlayerThrowDice(this, gameData);
    }
    
    void InitButtons() {
        if(isBtnInit) return;

        throwOneDice.onClick.AddListener(PlayerThrowOneDice);
        throwTwoDice.onClick.AddListener(PlayerThrowTwoDice);

        yesRestartBtn.onClick.AddListener(YesRestartThrowDice);
        noRestartBtn.onClick.AddListener(QuitState);

        isBtnInit = true;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        playerDicePanel.SetActive(false);
        
        isThrowDiceRestarted = false;
        turnState.Transactions();
    }

    void EndThrow() {
        if(!isThrowDiceRestarted && gameData.players[playerTurn].GetMonumentBuiltByType(typeof(RadioTower))) {
            restartPanel.SetActive(true);
        }
        else
            QuitState();
    }

    void YesRestartThrowDice() {
        playerDicePanel.SetActive(false);

        isThrowDiceRestarted = true;
        turnState.ThrowDice();
    }

    public void PlayerThrowOneDice() {
        playerDice.ThrowDice(1, gameData.players[playerTurn].ThrowDice, EndThrow);
        DisableBtn();
    }

    public void PlayerThrowTwoDice() {
        playerDice.ThrowDice(2, gameData.players[playerTurn].ThrowDice, EndThrow);
        DisableBtn();
    }

    void DisableBtn() {
        throwOneDice.interactable = false;
        throwTwoDice.interactable = false;
    }
}
