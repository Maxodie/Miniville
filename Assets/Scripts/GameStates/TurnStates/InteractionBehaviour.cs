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
    private int selectedPlayer;
    
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Start()
    {
        uiPanel.SetActive(true);
    }

    public void LoadPlayers()
    {
        for (int i = 0; i < gameData.players.Count; i++)
        {
            if (i == playerTurn)
                continue;

            Button btn = Object.Instantiate(playerChoiceButton, playerSelection).GetComponent<Button>();
            btn.onClick.AddListener(()=>
            {
                SelectPlayer(i);
            });   
        }
    }

    public void Update(float dt)
    {

    }

    public void QuitState()
    {
        
    }
    

    void SelectPlayer(int playerId)
    {
        
    }

    void ConfirmSelection()
    {
        turnState.Build();
    }
}
