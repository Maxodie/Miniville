using UnityEngine;

[System.Serializable]
public class TurnState : GameState {
    int currentPlayerId;
    ITurnState currentTurnState;

    //Temp
    [SerializeField] GameObject playerBoardPrefab;

    public override void InitGameState(ref GameData gameData){
        base.InitGameState(ref gameData);
    }

    public override void Start() {
        for(int i=0; i < gameData.players.Count; i++) {
            GameObject playerBoard = MonoBehaviour.Instantiate(playerBoardPrefab);
            playerBoard.transform.position += new Vector3(2 * i, 0, 0);
        }
    }

    public override void Update(float dt) {

    }

    public override void OnQuit() {
        base.OnQuit();
    }

    void PerformTurn() {

    }

    int ThrowDice() {
        return 0;
    }

    void Transactions(int diceResult) {

    }

    void Build() {
        
    }
}
