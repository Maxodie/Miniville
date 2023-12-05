using UnityEngine;
using TMPro;
using System.Collections;

[System.Serializable]
public class TransactionBehaviour : ITurnState {
    /*GameData gameData;
    int playerTurn;
    TurnState turnState;

    [SerializeField] GameObject transactionPanel;

    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        Start();
        QuitState();
    }

    public void Start() {
        transactionPanel.SetActive(true);
    }

    public void Update(float dt) {

    }

    public void QuitState() {
        transactionPanel.SetActive(false);
        turnState.Build();
    }

    void Transactions() {
        QuitState();
    }
    */
    
    [SerializeField] MonoBehaviour context;
    GameData gameData;
    int playerTurn;
    TurnState turnState;
    [SerializeField] GameObject turnInfoPanel; 
    [SerializeField] TMP_Text firstDice;
    [SerializeField] TMP_Text secondDice;
    WaitForSeconds waitForTransaction;
    [SerializeField] float timeBetweenTransactions = 1f;
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        // current player's id
        this.playerTurn = playerTurn;
        this.turnState = turnState;
        waitForTransaction = new WaitForSeconds(timeBetweenTransactions);

        Start();
    }

    public void Start() {
        ref int[] throwValue = ref gameData.players[playerTurn].throwValue;
        firstDice.text = "D1 : " + throwValue[0];
        if(throwValue.Length > 1)
            secondDice.text = "D2 : " + gameData.players[playerTurn].throwValue[1];
        else
            secondDice.text = "";

        turnInfoPanel.SetActive(true);
        context.StartCoroutine(PayTransaction());
    }

    public void Update(float dt)
    {
        
    }

    public void QuitState()
    {
        turnInfoPanel.SetActive(false);
        QuitPath();
    }

    private void QuitPath()
    {
        foreach (var establishment in gameData.players[playerTurn].buildingCards)
        {
            // if current player owns a business center, init the interaction state
            if (establishment.GetType() == typeof(BusinessCenter))
            {
                turnState.Interaction();
                return;
            }
        }
        // else init the build state
        turnState.Build();
    }

    IEnumerator PayTransaction()
    {
        Player currentPlayer;

        //Check red cards on other players 
        for(int i=gameData.players.Length-1; i >= 0; i--) {
            currentPlayer = gameData.players[i];

            //don't active currentPlayer red cards
            if(i != playerTurn) {
                yield return context.StartCoroutine(MoneyTransaction(CardPriority.FIRSt, currentPlayer));
            }
        }

        context.StartCoroutine(PlayerPaid());
    }

    IEnumerator MoneyTransaction(CardPriority cardPriority, Player currentPlayer)
    {
        foreach (var t in currentPlayer.buildingCards)
        {
            if(t.cardPriority == cardPriority && t.canPerformEffect(currentPlayer.totalThrowValue)) {
                Debug.Log("tr : " + gameData.players[playerTurn].playerBoard.name + cardPriority);
                t.PerformSpecial(gameData.players[playerTurn], currentPlayer, gameData.players);
                yield return waitForTransaction;
            }
        }
    }

    IEnumerator PlayerPaid() {
        Debug.Log("t");
        yield return context.StartCoroutine(MoneyTransaction(CardPriority.SECOND, gameData.players[playerTurn]));
        Debug.Log("tf");
        turnState.UpdateCoinText();
        QuitState();
    }
}
