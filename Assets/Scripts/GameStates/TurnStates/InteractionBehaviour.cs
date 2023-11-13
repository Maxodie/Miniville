using UnityEngine;
using UnityEngine.UI;

public class InteractionBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] Button SelectPlayerBtn; 
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        
    }

    void SelectPlayer() {

    }

    void ConfirmSelection() {
        
    }
}
