using UnityEngine;
public class GameState {
    protected GameData gameData;
    [HideInInspector] public Game game;
    [HideInInspector] public bool endState = false;
    
    public virtual void InitGameState(ref GameData gameData, Game game) {
        this.gameData = gameData;
        this.game = game;
    }

    public virtual void Start() {

    }

    public virtual void Update(float dt) {

    }

    public virtual void OnQuit() {
        endState = false;
    }

    protected void EndState() {
        endState = true;
        game.ChangeCurrentState();
    }
}
