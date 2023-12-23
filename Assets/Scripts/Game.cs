using UnityEngine;

// Class responsible for managing the game flow
public class Game : MonoBehaviour {
    int currentStateId; // Index of the current game state in the states array
    GameState[] states; // Array of game states
    GameData gameData; // Game data containing information about the game

    public int maxPlayer = 4; // Maximum number of players
    public int minPlayer = 2; // Minimum number of players

    // References to different game states in the game
    [SerializeField] StartGameState startGameState;
    [SerializeField] TurnState turnState;
    [SerializeField] EndGameState endGameState;
    
    [SerializeField] public UIData uiData; // UI data for the game

    // Awake method called when the script instance is being loaded
    void Awake() {
        gameData = new GameData(); // Initialize game data
    }

    // Start method called on the frame when a script is enabled
    void Start() {
        // Initialize game states and store them in the states array
        startGameState.InitGameState(ref gameData,  this);
        turnState.InitGameState(ref gameData, this);
        endGameState.InitGameState(ref gameData, this);
        states = new GameState[]{startGameState, turnState, endGameState};

        currentStateId = 0; // Set the initial state to the start game state
        states[currentStateId].Start(); // Start the initial game state
    }

    // Update method called every frame
    void Update() {
        states[currentStateId].Update(Time.deltaTime); // Update the current game state
        CheckForEndState(); // Check for the end state of the game
    }

    // Method to change the current game state
    public void ChangeCurrentState() {
        states[currentStateId].OnQuit(); // Perform actions when exiting the current state

        currentStateId++; // Move to the next state

        // If currentStateId exceeds the length of states array, reset it to 0 (looping)
        if(currentStateId > states.Length-1)
            currentStateId = 0;

        states[currentStateId].Start(); // Start the new current game state
    }
    
    // Method to check for the end state of the game
    void CheckForEndState() {
        if(states[currentStateId].endState) // If the current state has reached an end state
            ChangeCurrentState(); // Change to the next state
    }
}
