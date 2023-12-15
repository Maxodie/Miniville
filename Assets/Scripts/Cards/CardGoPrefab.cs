using UnityEngine;

// ScriptableObject defining the visual elements for a card in the game
[CreateAssetMenu(fileName ="CardGo", menuName ="Minivilles/CardGo")]
public class CardGoPrefab : ScriptableObject {
    public string cardName; // Name of the card prefab
    public GameObject cardGo; // Visual representation prefab for the card
    public GameObject buildingGo; // Visual representation prefab for the associated building
}
