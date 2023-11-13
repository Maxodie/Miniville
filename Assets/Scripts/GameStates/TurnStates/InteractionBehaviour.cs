using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InteractionBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject turnInfoPanel; 
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Start() {
        
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
