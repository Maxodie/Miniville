using UnityEngine;

// Class representing the game state
public class GameState {
    // Reference to game data
    protected GameData gameData;
    
    // Reference to the main game object (consider making it public only when necessary)
    [HideInInspector] public Game game;
    
    // Flag indicating the end state of the game
    [HideInInspector] public bool endState = false;
    
    // Method to initialize the game state
    public virtual void InitGameState(ref GameData gameData, Game game) {
        this.gameData = gameData;
        this.game = game;
    }

    // Method invoked when the game state starts
    public virtual void Start() {

    }

    // Method invoked on every frame update
    public virtual void Update(float dt) {

    }

    // Method invoked when exiting the game state
    public virtual void OnQuit() {
        endState = false;
    }

    // Method to end the current state and switch to another state
    protected void EndState() {
        endState = true;
        game.ChangeCurrentState(); // Change to the next game state
    }
}
