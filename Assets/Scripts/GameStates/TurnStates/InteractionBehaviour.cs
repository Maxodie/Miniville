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
        LoadPlayers();

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
            foreach (var card in t)
            {
                if (card.cardType == CardType.CITYLIFE)
                    continue;
                
                var cardCopy = card;
                Button btn = Object.Instantiate(cardChoiceButton, cardSelection).GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    selectedCard = cardCopy;
                    SelectOwnCard();
                });
            }
        }
    }

    void SelectOwnCard()
    {
        Dispose();
        foreach (var t in gameData.players[playerTurn].buildingCards)
        {
            foreach (var card in t)
            {
                if (card.cardType == CardType.CITYLIFE)
                    continue;
                
                var cardCopy = card;
                Button btn = Object.Instantiate(ownCardChoiceButton, cardSelection).GetComponent<Button>();
                btn.onClick.AddListener(() =>
                {
                    playerCard = cardCopy;
                    SwitchCards();
                });
            }
        }
    }

    void SwitchCards()
    {
        (selectedCard, playerCard) = (playerCard, selectedCard);
        QuitState();
    }

    void Dispose()
    {
        for(int i=0; i<playerSelection.childCount; i++)
        {
            Object.Destroy(playerSelection.GetChild(0));
        }

        for(int i=0; i<cardSelection.childCount; i++)
        {
            Object.Destroy(cardSelection.GetChild(0));
        }
    }

    void ConfirmSelection()
    {
        turnState.Build();
    }
}