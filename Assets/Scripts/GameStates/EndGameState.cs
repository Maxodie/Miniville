using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]

public class EndGameState : GameState
{

    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;
    [SerializeField] GameObject endPanel;

    bool initBtn = false;

    public override void InitGameState(ref GameData gameData, Game game){
        base.InitGameState(ref gameData, game);
        endPanel.SetActive(false);

        if(!initBtn)
            InitButtons();
    }
    
    void InitButtons() {
        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
        initBtn = true;
    }

    public override void Start() 
    {
        endPanel.SetActive(true);
    }

    public override void Update(float dt) 
    {

    }

    public override void OnQuit() {
        base.OnQuit();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    void QuitGame()
    {
        Application.Quit();
    }
}
