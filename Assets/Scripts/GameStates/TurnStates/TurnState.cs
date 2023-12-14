using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TurnState : GameState {
    int currentPlayerId;
    ITurnState currentTurnState;
    [SerializeField] float playerBoardDistanceToCenter = 10.5f;

    //Add to upml
    [SerializeField] ThrowDiceBehaviour throwDiceBehaviour;
    [SerializeField] InteractionBehaviour interactionBehaviour;
    [SerializeField] TransactionBehaviour transactionBehaviour;
    [SerializeField] BuildBehaviour buildBehaviour;
    //

    //Temp
    [SerializeField] GameObject playerBoardPrefab;
    [SerializeField] GameObject playerDicePanel;
    [SerializeField] Button endTurnBtn;

    public override void InitGameState(ref GameData gameData, Game game){
        base.InitGameState(ref gameData, game);
        
        InitButtons();
    }

    void InitButtons() {
        endTurnBtn.onClick.AddListener(FinishTurn);
    }

    public override void Start()
    {
        if(gameData.players[currentPlayerId].isRealPlayer)
            gameData.players[currentPlayerId].playerCanvas.SetActive(true);
            
        Vector3 boardPosition = new Vector3();
        Quaternion boardRotation = new Quaternion();
        
        //Load players board
        for(int i=0; i < gameData.players.Length; i++) {
            GameObject playerBoard = Object.Instantiate(playerBoardPrefab);
            
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
            gameData.players[i].Start(playerBoard);
        }

        PerformTurn();
    }

    public override void Update(float dt) {
        currentTurnState.Update(dt);
    }

    public override void OnQuit() {
        base.OnQuit();
    }

    public void PerformTurn() {
        gameData.players[currentPlayerId].playerCanvas.SetActive(true);
        gameData.players[currentPlayerId].playerFrame.SetCurrentActiveFrame(true);
        playerDicePanel.SetActive(true);

        if(WinCheck()) {
            gameData.winPlayerName = gameData.players[currentPlayerId].playerName;
            game.ChangeCurrentState();
            Debug.Log("win win on a win woow");
            return;
        }

        ThrowDice();
    }

    public void FinishTurn() {
        playerDicePanel.SetActive(false);

        SwitchCurrentPlayer();
    }

    bool WinCheck() {
        bool win = true;
        for(int i=0; i < gameData.players[currentPlayerId].monumentCards.Length; i++) {
            if(!gameData.players[currentPlayerId].monumentCards[i].built) {
                win = false;
            }
        }

        return win;
    }

    void SwitchCurrentPlayer() {
        if(gameData.players[currentPlayerId].canReplay) {
            gameData.players[currentPlayerId].canReplay = false;
            currentPlayerId --;
        }

        gameData.players[currentPlayerId].playerCanvas.SetActive(false);
        gameData.players[currentPlayerId].playerFrame.SetCurrentActiveFrame(false);
        
        currentPlayerId ++;

        if(currentPlayerId > gameData.players.Length -1)
            currentPlayerId = 0;

        PerformTurn();
    }

    public void ThrowDice() {
        //give to the player his current dice result
        currentTurnState = throwDiceBehaviour;
        throwDiceBehaviour.InitState(gameData, currentPlayerId, this, game.uiData);
    }

    public void Transactions() {
        currentTurnState = transactionBehaviour;
        transactionBehaviour.InitState(gameData, currentPlayerId, this);
    }

    public void Interaction()
    {
     
        currentTurnState = interactionBehaviour;
        interactionBehaviour.InitState(gameData, currentPlayerId, this);
    }

    public void Build() {
        currentTurnState = buildBehaviour;
        buildBehaviour.InitState(gameData, currentPlayerId, this);
    }
}
