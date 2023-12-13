using UnityEngine;
using TMPro;
using System.Collections;
using System.Linq;

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
        Player currentPlayer = gameData.players[playerTurn];
        for(int i=0; i < currentPlayer.buildingCards.Count; i++)
        {
            Establishment establishment = currentPlayer.buildingCards[i][0];
            // if current player owns a business center, init the interaction state
            if (establishment.GetType() == typeof(BusinessCenter))
            {
                Debug.Log("tt : " + currentPlayer.totalThrowValue);
                if(establishment.canPerformEffect(currentPlayer.totalThrowValue)) {
                    turnState.Interaction();
                    Debug.Log("tt");
                    return;
                }
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
                yield return context.StartCoroutine(MoneyTransaction(CardPriority.FIRSt, currentPlayer, gameData.players[playerTurn]));
            }
        }

        context.StartCoroutine(PlayerPaid());
    }

    IEnumerator MoneyTransaction(CardPriority cardPriority, Player currentPlayer, Player target)
    {
        for(int i=0; i < currentPlayer.buildingCards.Count; i++)
        {
            for(int j=0; j < currentPlayer.buildingCards[i].Count; j++)
            {//pn d'enfant qui appel pas perform effect
                if(currentPlayer.buildingCards[i][j].cardPriority == cardPriority && currentPlayer.buildingCards[i][j].canPerformEffect(currentPlayer.totalThrowValue)) {
                    currentPlayer.buildingCards[i][j].PerformSpecial(currentPlayer, target, gameData.players);
                    yield return waitForTransaction;
                }
            }
        }
    }

    IEnumerator PlayerPaid() {
        yield return context.StartCoroutine(MoneyTransaction(CardPriority.SECOND, gameData.players[0], gameData.players[playerTurn]));
        gameData.players[playerTurn].playerFrame.UpdateUI();
        QuitState();
    }
}
