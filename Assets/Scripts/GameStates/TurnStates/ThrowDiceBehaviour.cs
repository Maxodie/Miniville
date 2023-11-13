using System;
using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class ThrowDiceBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;
    [SerializeField] Button throwOneDice;
    [SerializeField] Button throwTwoDice;

    [SerializeField] GameObject playerDicePanel;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        InitButtons();

        playerDicePanel.SetActive(true);
    }
    
    void InitButtons() {
        throwOneDice.onClick.AddListener(PlayerThrowOneDice);
        throwTwoDice.onClick.AddListener(PlayerThrowTwoDice);
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        playerDicePanel.SetActive(false);
    }

    public void PlayerThrowOneDice() {
        gameData.players[playerTurn].ThrowDice(1);
    }

    public void PlayerThrowTwoDice() {
        gameData.players[playerTurn].ThrowDice(2);
    }
}
