using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {
    int currentStateId;
    GameState[] states;
    GameData gameData;

    int maxPlayer = 4;
    int playerMonumentNb = 4;
    [SerializeField] StartGameState startGameState;
    [SerializeField] TurnState turnState;
    [SerializeField] EndGameState endGameState;

    void Awake() {
        gameData = new GameData();
    }

    void Start() {
        startGameState.InitGameState(ref gameData);
        turnState.InitGameState(ref gameData);
        endGameState.InitGameState(ref gameData);
        states = new GameState[]{startGameState, turnState, endGameState};
        currentStateId = 0;
        states[currentStateId].Start();
    }

    void Update() {
        states[currentStateId].Update(Time.deltaTime);

        CheckForEndState();
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
