using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InteractionBehaviour : ITurnState {
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject turnInfoPanel; 
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Start() {
        PayTransaction();
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        
    }

    void PayTransaction() {
        Player currentPlayer;

        //Check red cards on other players 
        for(int i=gameData.players.Length-1; i >= 0; i--) {
            currentPlayer = gameData.players[i];

            //don't active currentPlayer red cards
            if(i != playerTurn) {
                MoneyTransaction(CardPriority.FIRSt, currentPlayer);
            }
        }

        PlayerPaid();
    }

    void MoneyTransaction(CardPriority cardPriority, Player currentPlayer) {
        Establishment currentEstablishment;

        for(int i=0; i < currentPlayer.buildingCards.Count; i++) {
            currentEstablishment = currentPlayer.buildingCards[i];

            if(currentEstablishment.cardPriority == cardPriority)
                currentEstablishment.PerformSpecial(currentPlayer, gameData.players[playerTurn]);
        }

        ConfirmSelection();
    }

    void PlayerPaid() {
        MoneyTransaction(CardPriority.SECOND, gameData.players[playerTurn]);
    }

    void SelectPlayer() {

    }

    void ConfirmSelection() {
        turnState.Build();
    }
}
