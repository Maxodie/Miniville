using UnityEngine;
public class BuildBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject transactionPanel;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        
    }

    void StartBuild() {

    }

    void EndBuild() {
        
    }
}
