public class ThrowDiceBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    public ThrowDiceBehaviour(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        
    }

    void SendDiceResultToTransactions() {

    }
}
