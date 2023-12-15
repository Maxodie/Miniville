using UnityEngine;

// Class handling behavior related to a card in the game
public class CardBehaviour {
    public Card card; // The card data associated with this behavior
    public CardGoPrefab cardGoPrefab; // Prefab containing visual elements for the card
    public GameObject spawnedGoCard; // Instantiated game object for the card
    Animator cardAnim; // Animator component for the card
    public GameObject spawnedGoBuilding; // Instantiated game object for the building (if applicable)

    // Constructor to initialize CardBehaviour with card and cardGoPrefab
    public CardBehaviour(Card card, CardGoPrefab cardGoPrefab) {
        this.card = card;
        this.cardGoPrefab = cardGoPrefab;
    }

    // Method to instantiate the card's visual representation
    public void InstantiateCard(Transform tr, Vector3 pos, bool activeBuilding) {
        if(spawnedGoCard) return; // If card is already spawned, exit the method

        // Instantiate the card's game object using the cardGoPrefab
        spawnedGoCard = Object.Instantiate(cardGoPrefab.cardGo, tr);
        spawnedGoCard.transform.localPosition = pos; // Set the position of the spawned card

        // Set the sprite for the card's visual representation
        spawnedGoCard.GetComponentInChildren<SpriteRenderer>().sprite = card.cardSprite;

        cardAnim = spawnedGoCard.GetComponent<Animator>(); // Get the Animator component

        if(activeBuilding)
            InstantiateBuilding(tr, pos); // If activeBuilding is true, instantiate the building
    }

    // Method to instantiate the building's visual representation
    public void InstantiateBuilding(Transform tr, Vector3 pos) {
        if(spawnedGoBuilding) return; // If building is already spawned, exit the method

        // Instantiate the building's game object using the building prefab from cardGoPrefab
        spawnedGoBuilding = Object.Instantiate(cardGoPrefab.buildingGo, tr);
        spawnedGoBuilding.transform.localPosition = pos; // Set the position of the spawned building
    }

    // Method to perform visual effects associated with the card using animations
    public void PerformVisualEffect() {
        cardAnim?.SetTrigger("ActiveEffect"); // Trigger an animation effect (if Animator is available)
    }
}
