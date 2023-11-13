using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StartGameState : GameState {
    [SerializeField] GameObject startPanel;
    [SerializeField] Button startBtn;
    
    // Cards given to new player
    [SerializeField] List<Card> initialDeck; // add to UML
    [SerializeField] Card[] initialMonuments; // add to UML

    //temp
    [SerializeField] Button addPlayerBtn;
    [SerializeField] TMPro.TMP_Text playerNbText;
    int playerNb = 1;

    public override void InitGameState(ref GameData gameData) {
        base.InitGameState(ref gameData);
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
            gameData.players.Add(new Player($"Player {i}", 4, 1, initialDeck, initialMonuments));

        EndState();
    }

    public void AddPlayerNb() {
        playerNb ++;
        playerNbText.text = $"Player : {playerNb}";
    }

    void PlayerSelection() {

    }
}
