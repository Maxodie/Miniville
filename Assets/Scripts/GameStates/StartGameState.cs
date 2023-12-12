using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StartGameState : GameState {
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject playerCanvasPrefab;
    [SerializeField] Button startBtn;
    
    // Cards given to new player
    [SerializeField] Establishment[] initialDeck; // add to UML

    //temp
    [SerializeField] Button addPlayerBtn;
    [SerializeField] Button removePlayerBtn;

    // UI
    [SerializeField] StartScreenManager startScreenManager;

    int playerNb = 2;

    public override void InitGameState(ref GameData gameData, Game game) {
        base.InitGameState(ref gameData, game);
        InitButtons();
        InitInitialEstablishments();
    }

    void InitButtons() {
        startBtn.onClick.AddListener(LoadPlayersBoards);
        addPlayerBtn.onClick.AddListener(AddPlayerNb);
        removePlayerBtn.onClick.AddListener(RemovePlayerNb);
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
        //playerNbText.text = $"Player : {playerNb}";
    }

    public override void Update(float dt) {

    }

    public override void OnQuit() {
        base.OnQuit();
    }

    void LoadPlayersBoards() {
        startPanel.SetActive(false);

        gameData.players = new Player[playerNb];

        playerCanvasPrefab.SetActive(false);

        for(int i=0; i<playerNb; i++) {
            //TODO load canvas and put it in player
            if(i==0)
                gameData.players[i] = new Player(true, $"Player {i}", 8, 2, 1, initialDeck.ToList(), gameData.monuments, playerCanvasPrefab, game.uiData.playerFrames[i], game.uiData.uIPlayerFrameScriptableObject);
            else
                gameData.players[i] = new AIPlayer(true, $"Player {i}", 4, 2, 1, initialDeck.ToList(), gameData.monuments, playerCanvasPrefab, game.uiData.playerFrames[i], game.uiData.uIPlayerFrameScriptableObject);
        }
        
        startScreenManager.SetupPlayerFrames(game.uiData, gameData);
        EndState();
    }

    public void AddPlayerNb() {
        if (playerNb >= game.maxPlayer) return;
        
        playerNb ++;
        startScreenManager.UpdatePlayerFrames(game.uiData, playerNb, addPlayerBtn, removePlayerBtn);
        //playerNbText.text = $"Player : {playerNb}";
    }
    
    public void RemovePlayerNb() {
        if (playerNb <= game.minPlayer) return;
        
        playerNb --;
        startScreenManager.UpdatePlayerFrames(game.uiData, playerNb, addPlayerBtn, removePlayerBtn);
        //playerNbText.text = $"Player : {playerNb}";
    }

    void PlayerSelection() {

    }
}
