using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StartGameState : GameState {
    [SerializeField] GameObject startPanel;
    [SerializeField] Button startBtn;
    
    // Cards given to new player
    [SerializeField] List<Establishment> initialDeck; // add to UML
    [SerializeField] Monument[] initialMonuments; // add to UML

    //temp
    [SerializeField] Button addPlayerBtn;
    [SerializeField] TMP_Text playerNbText;
    int playerNb = 1;

    public override void InitGameState(ref GameData gameData, Game game) {
        base.InitGameState(ref gameData, game);
        InitButtons();
    }

    void InitButtons() {
        startBtn.onClick.AddListener(LoadPlayersBoards);
        addPlayerBtn.onClick.AddListener(AddPlayerNb);
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

        for(int i=0; i<playerNb; i++)
            gameData.players.Add(new Player($"Player {i}", 4, 2, 1, initialDeck, initialMonuments));

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
