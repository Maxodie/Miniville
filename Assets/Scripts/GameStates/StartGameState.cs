using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Serializable class representing the starting game state
[System.Serializable]
public class StartGameState : GameState {
    [SerializeField] GameObject startPanel; // Reference to the start panel GameObject
    [SerializeField] GameObject playerCanvasPrefab; // Prefab for player canvas
    [SerializeField] Button startBtn; // Button to start the game
    
    // Cards given to new player
    [SerializeField] Establishment[] initialDeck; // Array of initial establishments for players (add to UML)

    // Temporary buttons for adding/removing players
    [SerializeField] Button addPlayerBtn;
    [SerializeField] Button removePlayerBtn;

    // UI manager for the start screen
    [SerializeField] StartScreenManager startScreenManager;

    int playerNb = 2; // Initial number of players

    // Method to initialize the game state
    public override void InitGameState(ref GameData gameData, Game game) {
        base.InitGameState(ref gameData, game);
        InitButtons();
        InitInitialEstablishments();
    }

    // Method to initialize buttons' click listeners
    void InitButtons() {
        startBtn.onClick.AddListener(LoadPlayersBoards);
        addPlayerBtn.onClick.AddListener(AddPlayerNb);
        removePlayerBtn.onClick.AddListener(RemovePlayerNb);
    }

    // Method to initialize the initial establishments for players
    void InitInitialEstablishments() {
        // Count the number of establishments that are marked as starting cards
        initialDeck = new Establishment[gameData.establishments.Count(k => k.Key.startCard)];
        Establishment[] keys = gameData.establishments.Keys.ToArray();
        int j = 0;
        for (int i = 0; i < gameData.establishments.Count; i++) {
            if (keys[i].startCard) {
                initialDeck[j] = keys[i];
                j++;
            }
        }
    }

    // Method invoked when the game state starts
    public override void Start() {
        // Perform actions at the start of the state
    }

    // Method invoked on every frame update
    public override void Update(float dt) {
        // Perform actions in each frame update
    }

    // Method invoked when exiting the game state
    public override void OnQuit() {
        base.OnQuit();
    }

    // Method to load player boards and start the game
    void LoadPlayersBoards() {
        startPanel.SetActive(false); // Hide the start panel

        gameData.players = new Player[playerNb]; // Initialize the array of players

        playerCanvasPrefab.SetActive(false); // Disable the player canvas prefab

        for (int i = 0; i < playerNb; i++) {
            // Create players with initial settings and decks
            if (i == 0)
                gameData.players[i] = new Player(true, $"Player {i}", 3, 2, 1, initialDeck.ToList(), gameData.monuments, playerCanvasPrefab, game.uiData.playerFrames[i], game.uiData.uIPlayerFrameScriptableObject);
            else
                gameData.players[i] = new AIPlayer(true, $"Player {i}", 3, 2, 1, initialDeck.ToList(), gameData.monuments, playerCanvasPrefab, game.uiData.playerFrames[i], game.uiData.uIPlayerFrameScriptableObject);
        }
        
        // Setup player frames on the UI
        startScreenManager.SetupPlayerFrames(game.uiData, gameData);
        EndState(); // End the current state
    }

    // Method to increase the number of players
    public void AddPlayerNb() {
        if (playerNb >= game.maxPlayer) return;
        
        playerNb++;
        startScreenManager.UpdatePlayerFrames(game.uiData, playerNb, addPlayerBtn, removePlayerBtn);
    }
    
    // Method to decrease the number of players
    public void RemovePlayerNb() {
        if (playerNb <= game.minPlayer) return;
        
        playerNb--;
        startScreenManager.UpdatePlayerFrames(game.uiData, playerNb, addPlayerBtn, removePlayerBtn);
    }

    // Method for player selection (to be implemented)
    void PlayerSelection() {
        // Add player selection logic here
    }
}
