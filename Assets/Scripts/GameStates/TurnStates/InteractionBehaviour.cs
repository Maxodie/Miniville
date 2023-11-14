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
    [SerializeField] 
    [SerializeField] private Button playerChoiceButton;
    private int selectedPlayer;
    
    
    [SerializeField] GameObject turnInfoPanel; 
    public void InitState(GameData gameData, int playerTurn, TurnState turnState) {
        this.gameData = gameData;
        this.playerTurn = playerTurn;
        this.turnState = turnState;
    }

    public void Start()
    {
        turnInfoPanel.SetActive(true);
    }

    public void Update(float dt)
    {

    }

    public void QuitState()
    {
        turnInfoPanel.SetActive(false);
    }
    

    void SelectPlayer()
    {
        
    }

    void ConfirmSelection()
    {
        turnState.Build();
    }
}
