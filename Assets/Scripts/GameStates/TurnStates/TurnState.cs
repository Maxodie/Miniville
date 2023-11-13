using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurnState : GameState {
    int currentPlayerId;
    ITurnState currentTurnState;

    //Add to upml
    [SerializeField] ThrowDiceBehaviour throwDiceBehaviour;
    [SerializeField] InteractionBehaviour interactionBehaviour;
    [SerializeField] TransactionBehaviour transactionBehaviour;
    [SerializeField] BuildBehaviour buildBehaviour;
    //

    //Temp
    [SerializeField] GameObject playerBoardPrefab;
    [SerializeField] GameObject playerDicePanel;
    [SerializeField] Button throwDiceBtn;
    [SerializeField] Button endTurnBtn;

    public override void InitGameState(ref GameData gameData, Game game){
        base.InitGameState(ref gameData, game);
        playerDicePanel.SetActive(false);
        InitButtons();
    }

    void InitButtons() {//add to uml
        throwDiceBtn.onClick.AddListener(ThrowDice);
        endTurnBtn.onClick.AddListener(FinishTurn);
    }

    public override void Start() {
        //Load players board
        for(int i=0; i < gameData.players.Count; i++) {
            GameObject playerBoard = MonoBehaviour.Instantiate(playerBoardPrefab);
            playerBoard.transform.position += new Vector3(2 * i, 0, 0);
        }

        PerformTurn();
    }

    public override void Update(float dt) {

    }

    public override void OnQuit() {
        base.OnQuit();
    }

    void PerformTurn() {
        playerDicePanel.SetActive(true);

        throwDiceBehaviour.InitState(gameData, currentPlayerId, this);
        interactionBehaviour.InitState(gameData, currentPlayerId, this);
        transactionBehaviour.InitState(gameData, currentPlayerId, this);
        buildBehaviour.InitState(gameData, currentPlayerId, this);
    }

    void FinishTurn() {//Add to uml
        playerDicePanel.SetActive(false);
        SwitchCurrentPlayer();
    }

    void SwitchCurrentPlayer() {//Add to uml
        currentPlayerId ++;

        if(currentPlayerId > gameData.players.Count -1)
            currentPlayerId = 0;

        PerformTurn();
    }

    void ThrowDice() {
        //give to the player his current dice result
        currentTurnState = throwDiceBehaviour;
    }

    void Transactions(int diceResult) {
        currentTurnState = transactionBehaviour;
    }

    void Build() {
        currentTurnState = buildBehaviour;
    }
}
