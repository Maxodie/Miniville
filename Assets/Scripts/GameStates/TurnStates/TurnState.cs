using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Serializable class representing the current state of a turn in the game
[System.Serializable]
public class TurnState : GameState {
    int currentPlayerId;
    ITurnState currentTurnState;
    [SerializeField] float playerBoardDistanceToCenter = 10.5f;

    // Components for various turn behaviors (add to UML)
    [SerializeField] ThrowDiceBehaviour throwDiceBehaviour;
    [SerializeField] InteractionBehaviour interactionBehaviour;
    [SerializeField] TransactionBehaviour transactionBehaviour;
    [SerializeField] BuildBehaviour buildBehaviour;
    //

    // Temporary variables
    [SerializeField] GameObject playerBoardPrefab;
    [SerializeField] GameObject playerDicePanel;
    [SerializeField] Button endTurnBtn;

    // Method to initialize the game state with necessary data
    public override void InitGameState(ref GameData gameData, Game game){
        base.InitGameState(ref gameData, game);
        
        InitButtons();
    }

    // Method to initialize buttons
    void InitButtons() {
        endTurnBtn.onClick.AddListener(FinishTurn);
    }

    // Method to start the turn state
    public override void Start()
    {
        if(gameData.players[currentPlayerId].isRealPlayer)
            gameData.players[currentPlayerId].playerCanvas.SetActive(true);
            
        Vector3 boardPosition = new Vector3();
        Quaternion boardRotation = new Quaternion();
        
        // Load players' boards
        for(int i=0; i < gameData.players.Length; i++) {
            GameObject playerBoard = Object.Instantiate(playerBoardPrefab);
            
            // Define position and rotation of players' boards
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
            
            // Apply position and rotation to the player's board
            playerBoard.transform.position = boardPosition;
            playerBoard.transform.rotation = boardRotation;
            gameData.players[i].Start(playerBoard);
        }

        PerformTurn();
    }

    // Method to update the turn state
    public override void Update(float dt) {
        currentTurnState.Update(dt);
    }

    // Method invoked upon quitting the turn state
    public override void OnQuit() {
        base.OnQuit();
    }

    // Method to perform a turn
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

    // Method to finish the current turn
    public void FinishTurn() {
        playerDicePanel.SetActive(false);

        SwitchCurrentPlayer();
    }

    // Method to check for a win condition
    bool WinCheck() {
        bool win = true;
        for(int i=0; i < gameData.players[currentPlayerId].monumentCards.Length; i++) {
            if(!gameData.players[currentPlayerId].monumentCards[i].built) {
                win = false;
            }
        }

        return win;
    }

    // Method to switch to the next player's turn
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

    // Method to initiate the dice throwing phase
    public void ThrowDice() {
        currentTurnState = throwDiceBehaviour;
        throwDiceBehaviour.InitState(gameData, currentPlayerId, this, game.uiData);
    }

    // Method to initiate the transaction phase
    public void Transactions() {
        currentTurnState = transactionBehaviour;
        transactionBehaviour.InitState(gameData, currentPlayerId, this);
    }

    // Method to initiate the interaction phase
    public void Interaction()
    {
        currentTurnState = interactionBehaviour;
        interactionBehaviour.InitState(gameData, currentPlayerId, this);
    }

    // Method to initiate the building phase
    public void Build() {
        currentTurnState = buildBehaviour;
        buildBehaviour.InitState(gameData, currentPlayerId, this);
    }
}
