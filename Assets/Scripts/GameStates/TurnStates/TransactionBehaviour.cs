using UnityEngine;

[System.Serializable]
public class TransactionBehaviour : ITurnState {
    /*GameData gameData;
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
    */
    
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject turnInfoPanel; 
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        // current player's id
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Start() {
        turnInfoPanel.SetActive(true);
        PayTransaction();
    }

    public void Update(float dt)
    {
        
    }

    public void QuitState()
    {
        turnInfoPanel.SetActive(false);
        QuitPath();
    }

    private void QuitPath()
    {
        foreach (var establishment in gameData.players[playerTurn].buildingCards)
        {
            // if current player owns a business center, init the interaction state
            if (establishment.GetType() == typeof(BusinessCenter))
            {
                turnState.Interaction();
                return;
            }
        }
        // else init the build state
        turnState.Build();
    }

    void PayTransaction()
    {
        Player currentPlayer;

        //Check red cards on other players 
        for(int i=gameData.players.Length-1; i >= 0; i--) {
            currentPlayer = gameData.players[i];

            //don't active currentPlayer red cards
            if(i != playerTurn) {
                MoneyTransaction(CardPriority.FIRSt, currentPlayer);
            }
        }

        PlayerPaid();
    }

    void MoneyTransaction(CardPriority cardPriority, Player currentPlayer)
    {
        foreach (var t in currentPlayer.buildingCards)
        {
            if(t.cardPriority == cardPriority)
                t.PerformSpecial(gameData.players[playerTurn], currentPlayer, gameData.players);
        }
    }

    void PlayerPaid() {
        MoneyTransaction(CardPriority.SECOND, gameData.players[playerTurn]);
        QuitState();
    }
}
