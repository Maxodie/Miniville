using UnityEngine;

public class Game : MonoBehaviour {
    int currentStateId;
    GameState[] states;
    GameData gameData;

    public int maxPlayer = 4;
    public int minPlayer = 2;
    [SerializeField] StartGameState startGameState;
    [SerializeField] TurnState turnState;
    [SerializeField] EndGameState endGameState;
    
    [SerializeField]  public UIData uiData;

    void Awake() {
        gameData = new GameData();
    }

    void Start() {
        startGameState.InitGameState(ref gameData,  this);
        turnState.InitGameState(ref gameData, this);
        endGameState.InitGameState(ref gameData, this);
        states = new GameState[]{startGameState, turnState, endGameState};
        currentStateId = 0;
        states[currentStateId].Start();
    }

    void Update() {
        states[currentStateId].Update(Time.deltaTime);
    }

    public void ChangeCurrentState() {
        states[currentStateId].OnQuit();

        currentStateId ++;

        if(currentStateId > states.Length-1)
            currentStateId = 0;

        states[currentStateId].Start();
    }
    
    void CheckForEndState() {
        if(states[currentStateId].endState)
            ChangeCurrentState();
    }
}
