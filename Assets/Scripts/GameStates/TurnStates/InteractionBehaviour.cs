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
    
    public void InitState(GameData gameData, int playerTurn, TurnState turnState)
    {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;

        if(!isBtnInit) {
            InitButton();
        }

        Start();
    }

    public void Start()
    {
        uiPanel.SetActive(true);
        LoadPlayers();

        gameData.players[playerTurn].OptionalPlayerInteraction(this, gameData);
    }

    void InitButton() {
        stopBtn.onClick.AddListener(() => QuitState());
        isBtnInit = true;
    }

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

    public void SwitchCards()
    {
        gameData.players[playerTurn].ExchangeCard(playerCardId, gameData.players[selectedPlayer], selectedCardId);
        QuitState();
    }

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

    void ConfirmSelection()
    {
        turnState.Build();
    }
    
    public void DisplayUpSizedCard(bool value, Sprite imageCard = null)
    {
        if (value)
            upSizedCard.sprite = imageCard;
        
        upSizedCard.gameObject.SetActive(value);
    }
}