using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]

public class EndGameState : GameState
{

    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    public override void InitGameState(ref GameData gameData, Game game){
        base.InitGameState(ref gameData, game);
    }
    
    void InitButtons() {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public override void Start() 
    {

    }

    public override void Update(float dt) 
    {

    }

    public override void OnQuit() {
        base.OnQuit();
    }

    void RestartGame()
    {
        SceneManager.LoadScene("Pierre");
    }
    
    void QuitGame()
    {
        Application.Quit();
    }
}
