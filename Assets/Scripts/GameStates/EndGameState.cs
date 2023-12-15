using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class EndGameState : GameState
{
    // Serialized fields accessible in the Unity Editor
    [SerializeField] Button restartButton; // Button to restart the game
    [SerializeField] TMP_Text winText; // Text displaying the winner's name
    [SerializeField] Button quitButton; // Button to quit the game
    [SerializeField] GameObject endPanel; // Panel displaying end game UI

    bool initBtn = false; // Flag to check if buttons have been initialized

    // Initializing the game state
    public override void InitGameState(ref GameData gameData, Game game)
    {
        base.InitGameState(ref gameData, game);
        endPanel.SetActive(false); // Deactivating end panel on initialization

        // Check if buttons are not yet initialized, then initialize them
        if (!initBtn)
            InitButtons();
    }
    
    // Method to initialize buttons with their respective click listeners
    void InitButtons()
    {
        restartButton.onClick.AddListener(RestartGame); // Add listener for restart button
        quitButton.onClick.AddListener(QuitGame); // Add listener for quit button
        initBtn = true; // Set the initialization flag to true
    }

    // Executed when the game state starts
    public override void Start()
    {
        endPanel.SetActive(true); // Activating the end panel
        winText.text = "Winner : " + gameData.winPlayerName; // Displaying winner's name
    }

    // Update method, currently empty and not utilized
    public override void Update(float dt)
    {
        // You can add logic for continuous updates here if needed
    }

    // Executed when the game is quitting (not currently used)
    public override void OnQuit()
    {
        base.OnQuit(); // Call the base class OnQuit method
        // Additional cleanup or actions on game quitting can be added here
    }

    // Method to restart the game by reloading the current scene
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    // Method to quit the application
    void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}
