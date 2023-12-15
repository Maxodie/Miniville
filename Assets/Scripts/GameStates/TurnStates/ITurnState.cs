// Interface representing a turn state in the game
public interface ITurnState {
    public void Update(float dt); // Method to update the state
    public void Start(); // Method to start the state
    public void QuitState(); // Method to exit the state
}