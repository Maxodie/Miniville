[System.Serializable]
public class EndGameState : GameState {

    public override void InitGameState(ref GameData gameData, Game game){
        base.InitGameState(ref gameData, game);
    }

    public override void Start() 
    {

    }

    public override void Update(float dt) 
    {

    }

    public override void OnQuit() {
        base.OnQuit();
    }
}
