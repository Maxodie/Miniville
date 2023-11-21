using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StartGameState : GameState {
    [SerializeField] GameObject startPanel;
    [SerializeField] Button startBtn;
    
    // Cards given to new player
    [SerializeField] Establishment[] initialDeck; // add to UML

    //temp
    [SerializeField] Button addPlayerBtn;
    [SerializeField] TMP_Text playerNbText;

    //MainMenu UI
    [SerializeField] StartScreenManager startScreenManager;

    int playerNb = 1;

    public override void InitGameState(ref GameData gameData, Game game) {
        base.InitGameState(ref gameData, game);
        InitButtons();
        InitInitialEstablishments();
    }

    void InitButtons() {
        startBtn.onClick.AddListener(LoadPlayersBoards);
        addPlayerBtn.onClick.AddListener(AddPlayerNb);
    }

    void InitInitialEstablishments()
    {
        initialDeck = new Establishment[gameData.establishments.Count(k => k.Key.startCard)];
        Establishment[] keys = gameData.establishments.Keys.ToArray();
        int j = 0;
        for (int i = 0; i < gameData.establishments.Count; i++) 
        {
            
            if(keys[i].startCard) {
                initialDeck[j] = keys[i];
                j++;
            }
        }
    }

    public override void Start() {
        startPanel.SetActive(true);
        playerNbText.text = $"Player : {playerNb}";
    }

    public override void Update(float dt) {

    }

    public override void OnQuit() {
        base.OnQuit();
    }

    void LoadPlayersBoards() {
        startPanel.SetActive(false);

        gameData.players = new Player[playerNb];
        for(int i=0; i<playerNb; i++)
            gameData.players[i] = new Player($"Player {i}", 4, 2, 1, initialDeck.ToList(), gameData.monuments);

        EndState();
    }

    public void AddPlayerNb() {
        if (playerNb >= game.maxPlayer) return;
        
        playerNb ++;
        playerNbText.text = $"Player : {playerNb}";
    }

    void PlayerSelection() {

    }
}
