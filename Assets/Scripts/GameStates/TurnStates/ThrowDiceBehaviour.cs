using UnityEngine;

[System.Serializable]
public class ThrowDiceBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    public ThrowDiceBehaviour(GameData gameData, int playerTurn, TurnState turnState) {//moettre ça dans play turn
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        
    }

    public void SendDiceResultToTransactions() {
        gameData.players[playerTurn].currentDice = Random.Range(0, 7);
    }
}
