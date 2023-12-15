using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;

// Serializable class implementing behavior for handling transactions during a turn state
[System.Serializable]
public class TransactionBehaviour : ITurnState {
    [SerializeField] MonoBehaviour context; // Reference to a MonoBehaviour context
    GameData gameData;
    int playerTurn;
    TurnState turnState;
    [SerializeField] GameObject turnInfoPanel; // Panel to display turn information
    [SerializeField] TMP_Text firstDice; // Text field for the first dice value
    [SerializeField] TMP_Text secondDice; // Text field for the second dice value
    WaitForSeconds waitForTransaction; // WaitForSeconds object for transaction delays
    [SerializeField] float timeBetweenTransactions = 1f; // Time delay between transactions

    // Method to initialize the state with necessary data
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
        waitForTransaction = new WaitForSeconds(timeBetweenTransactions);

        Start();
    }

    // Method invoked at the start of the transaction phase
    public void Start() {
        ref int[] throwValue = ref gameData.players[playerTurn].throwValue;
        firstDice.text = "D1 : " + throwValue[0];
        if (throwValue.Length > 1)
            secondDice.text = "D2 : " + gameData.players[playerTurn].throwValue[1];
        else
            secondDice.text = "";

        turnInfoPanel.SetActive(true);
        context.StartCoroutine(PayTransaction());
    }

    // Method for update operations during the transaction phase
    public void Update(float dt) {
        // Perform necessary update operations if any
    }

    // Method to exit the transaction state
    public void QuitState() {
        turnInfoPanel.SetActive(false);
        QuitPath();
    }

    // Method to handle the appropriate game state after transactions
    private void QuitPath() {
        Player currentPlayer = gameData.players[playerTurn];
        for (int i = 0; i < currentPlayer.buildingCards.Count; i++) {
            Establishment establishment = currentPlayer.buildingCards[i][0];
            // if current player owns a business center, init the interaction state
            if (establishment.GetType() == typeof(BusinessCenter)) {
                if (establishment.canPerformEffect(currentPlayer.totalThrowValue)) {
                    turnState.Interaction();
                    return;
                }
            }
        }
        // else init the build state
        turnState.Build();
    }

    // Coroutine for handling money transactions among players
    IEnumerator PayTransaction() {
        Player currentPlayer;

        // Check red cards on other players
        for (int i = gameData.players.Length - 1; i >= 0; i--) {
            currentPlayer = gameData.players[i];

            // Don't activate currentPlayer red cards
            if (i != playerTurn) {
                yield return context.StartCoroutine(MoneyTransaction(CardPriority.FIRSt, currentPlayer, gameData.players[playerTurn]));
            }
        }

        context.StartCoroutine(PlayerPaid());
    }

    // Coroutine for handling money transactions based on card priority
    IEnumerator MoneyTransaction(CardPriority cardPriority, Player currentPlayer, Player target) {
        for (int i = 0; i < currentPlayer.buildingCards.Count; i++) {
            for (int j = 0; j < currentPlayer.buildingCards[i].Count; j++) {
                if (currentPlayer.buildingCards[i][j].cardPriority == cardPriority && currentPlayer.buildingCards[i][j].canPerformEffect(currentPlayer.totalThrowValue)) {
                    currentPlayer.buildingCards[i][j].PerformSpecial(currentPlayer, target, gameData.players);
                    yield return waitForTransaction;
                }
            }
        }
    }

    // Coroutine to handle player payments after transactions
    IEnumerator PlayerPaid() {
        yield return context.StartCoroutine(MoneyTransaction(CardPriority.SECOND, gameData.players[0], gameData.players[playerTurn]));
        gameData.players[playerTurn].playerFrame.UpdateUI();
        QuitState();
    }
}
