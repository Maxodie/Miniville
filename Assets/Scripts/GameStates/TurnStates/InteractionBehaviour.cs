using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InteractionBehaviour : ITurnState 
{
    GameData gameData;
    [HideInInspector] public int playerTurn;
    TurnState turnState;

    [Header("TransactionUi")]
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Transform playerSelection;
    [SerializeField] private Transform cardSelection;
    [SerializeField] private GameObject playerChoiceButton;
    [SerializeField] CardUIData cardUISelectPrefab;
    [SerializeField] Button stopBtn;
    [SerializeField] Image upSizedCard;
    [SerializeField] TMP_Text textinfos;
    bool isBtnInit = false;

    int playerCardId;
    int selectedCardId;
    private int selectedPlayer;
    
    // Initializing the InteractionBehaviour state
    public void InitState(GameData gameData, int playerTurn, TurnState turnState)
    {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        // If buttons are not initialized, initialize them
        if (!isBtnInit) {
            InitButton();
        }

        Start();
    }

    // Starting the interaction state
    public void Start()
    {
        uiPanel.SetActive(true);
        LoadPlayers();

        gameData.players[playerTurn].OptionalPlayerInteraction(this, gameData);
    }

    // Initializing the stop button
    void InitButton() {
        stopBtn.onClick.AddListener(() => QuitState());
        isBtnInit = true;
    }

    // Loading the player choices for interaction
    public void LoadPlayers()
    {
        for (int i = 0; i < gameData.players.Length; i++)
        {
            if (i == playerTurn)
                continue;

            int iCopy = i;

            Button btn = Object.Instantiate(playerChoiceButton, playerSelection).GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                SelectTargetCard(iCopy);
            });   

            btn.GetComponentInChildren<TMP_Text>().text = "Player : " + i;
            textinfos.text = "Choose a player to exchange card with";
        }
    }

    // Updating the interaction state
    public void Update(float dt)
    {

    }

    // Quitting the interaction state
    public void QuitState()
    {
        Dispose();
        uiPanel.SetActive(false);
        turnState.Build();
    }
    
    // Selecting the target card for interaction
    void SelectTargetCard(int playerId)
    {
        Dispose();
        selectedPlayer = playerId;
        textinfos.text = "Select the target card";
        for (int j=0; j < gameData.players[playerId].buildingCards.Count; j++)
        {
            Establishment cards = gameData.players[playerId].buildingCards[j][0];
            
            if (cards.cardType == CardType.CITYLIFE)
                continue;
            
            int jCopy = j;
            CardUIPrefab cardUIPrefab = new CardUIPrefab(cardUISelectPrefab, cardSelection, cards, turnState.game, null, this);
            cardUIPrefab.loadedGo.GetComponent<Button>().onClick.AddListener(() =>
            {
                selectedCardId = jCopy;
                SelectOwnCard();
            });
        }
    }

    // Selecting the own card for interaction
    void SelectOwnCard()
    {
        Dispose();
        textinfos.text = "Choose one of your cards";
        for (int j=0; j < gameData.players[playerTurn].buildingCards.Count; j++)
        {
            Establishment cards = gameData.players[playerTurn].buildingCards[j][0];
            if (cards.cardType == CardType.CITYLIFE)
                continue;
            
            int jCopy = j;
            CardUIPrefab cardUIPrefab = new CardUIPrefab(cardUISelectPrefab, cardSelection, cards, turnState.game, null, this);
            cardUIPrefab.loadedGo.GetComponent<Button>().onClick.AddListener(() =>
            {
                playerCardId = jCopy;
                SwitchCards();
            });
        }
    }

    // Switching the cards between players
    public void SwitchCards()
    {
        gameData.players[playerTurn].ExchangeCard(playerCardId, gameData.players[selectedPlayer], selectedCardId);
        QuitState();
    }

    // Disposing of buttons and card selections
    void Dispose()
    {
        for(int i=0; i<playerSelection.childCount; i++)
        {
            Object.Destroy(playerSelection.GetChild(i).gameObject);
        }

        for(int i=0; i<cardSelection.childCount; i++)
        {
            Object.Destroy(cardSelection.GetChild(i).gameObject);
        }
        DisplayUpSizedCard(false);
    }

    // Confirming the card selection and continuing to build state
    void ConfirmSelection()
    {
        turnState.Build();
    }
    
    // Displaying the upsized card
    public void DisplayUpSizedCard(bool value, Sprite imageCard = null)
    {
        if (value)
            upSizedCard.sprite = imageCard;
        
        upSizedCard.gameObject.SetActive(value);
    }
}
