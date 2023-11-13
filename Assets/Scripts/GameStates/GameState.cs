using UnityEngine;
public class GameState {
    protected GameData gameData;
    [HideInInspector] public bool endState = false;
    
    public virtual void InitGameState(ref GameData gameData) {
        this.gameData = gameData;
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
    }
}
