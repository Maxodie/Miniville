using System;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurnState : GameState {
    int currentPlayerId;
    ITurnState currentTurnState;
    float playerBoardDistanceToCenter = 4.5f;

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

    public override void Start()
    {
        Vector3 boardPosition = new Vector3();
        Quaternion boardRotation = new Quaternion();
        
        //Load players board
        for(int i=0; i < gameData.players.Count; i++) {
            GameObject playerBoard = MonoBehaviour.Instantiate(playerBoardPrefab);
            
            // Define position and rotation of players' board
            switch (i)
            {
                case 0:
                    boardPosition = new Vector3(playerBoardDistanceToCenter, 0, 0);
                    boardRotation = Quaternion.Euler(0,180,0);
                    break;
                case 1:
                    boardPosition = new Vector3(-playerBoardDistanceToCenter, 0, 0);
                    boardRotation = Quaternion.Euler(0,0,0);
                    break;
                case 2:
                    boardPosition = new Vector3(0, 0, playerBoardDistanceToCenter);
                    boardRotation = Quaternion.Euler(0,90,0);
                    break;
                case 3:
                    boardPosition = new Vector3(0, 0, -playerBoardDistanceToCenter);
                    boardRotation = Quaternion.Euler(0,-90,0);
                    break;
            }
            
            // Apply position and rotation
            playerBoard.transform.position = boardPosition;
            playerBoard.transform.rotation = boardRotation;
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
    }

    void FinishTurn() {//Add to uml
        playerDicePanel.SetActive(false);
        SwitchCurrentPlayer();
    }

    void SwitchCurrentPlayer() {//Add to uml
        currentPlayerId ++;

        if(currentPlayerId > gameData.players.Count -1)
            currentPlayerId = 0;
    }

    void ThrowDice() {
        //give to the player his current dice result
    //    gameData.players[currentPlayerId].currentDice = Random.Range(0, 7);
    }

    void Transactions(int diceResult) {

    }

    void Build() {
        
    }
}
