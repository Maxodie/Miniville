using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InteractionBehaviour : ITurnState 
{
    GameData gameData;
    int playerTurn;
    TurnState turnState;

    [Header("TransactionUi")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Transform playerSelection;
    [SerializeField] private Transform cardSelection;
    [SerializeField] private GameObject playerChoiceButton;
    [SerializeField] private GameObject cardChoiceButton;
    [SerializeField] private GameObject ownCardChoiceButton;

    private Card selectedCard;
    private Card playerCard;
    private int selectedPlayer;
    
    public void InitState(GameData gameData, int playerTurn, TurnState turnState)
    {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        Start();
    }

    public void Start()
    {
        uiPanel.SetActive(true);
        gameData.players[playerTurn].OptionalPlayerInteraction(this);
    }

    public void LoadPlayers()
    {
        for (int i = 0; i < gameData.players.Length; i++)
        {
            if (i == playerTurn)
                continue;

            Button btn = Object.Instantiate(playerChoiceButton, playerSelection).GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                SelectTargetCard(i);
            });   
        }
    }

    public void Update(float dt)
    {

    }

    public void QuitState()
    {
        Dispose();
        uiPanel.SetActive(false);
        turnState.Build();
    }
    
    void SelectTargetCard(int playerId)
    {
        Dispose();
        foreach (var t in gameData.players[playerId].buildingCards)
        {
            if (t.cardType == CardType.CITYLIFE)
                continue;
            
            Button btn = Object.Instantiate(cardChoiceButton, cardSelection).GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                selectedCard = t;
                SelectOwnCard();
            });
        }
    }

    void SelectOwnCard()
    {
        Dispose();
        foreach (var t in gameData.players[playerTurn].buildingCards)
        {
            if (t.cardType == CardType.CITYLIFE)
                continue;
            
            Button btn = Object.Instantiate(ownCardChoiceButton, cardSelection).GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                playerCard = t;
                SwitchCards();
            });
        }
    }

    void SwitchCards()
    {
        (selectedCard, playerCard) = (playerCard, selectedCard);
        QuitState();
    }

    void Dispose()
    {
        foreach (GameObject button in playerSelection)
        {
            Object.Destroy(button);
        }

        foreach (GameObject button in cardSelection)
        {
            Object.Destroy(button);
        }
    }

    void ConfirmSelection()
    {
        turnState.Build();
    }
}