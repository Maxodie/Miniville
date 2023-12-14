public class GroceryStore : GreenCard
{
    public GroceryStore(
        CardGoPrefab cardGoPrefab, // GameObject prefab for the card's graphical representation
        string cardImgPath, // Path to the card's image
        string cardName, // Name of the card
        CardType cardType, // Type of the card
        string cardEffectDescription, // Description of the card's effect
        int constructionCost, // Cost to construct/build the card
        int gains, // Coins gained when the card's effect is triggered
        CardType requiredCardType, // Type of card required for effect
        CardPriority cardPriority, // Priority of the card
        int[] requiredDiceValues, // Array of dice values required for construction
        bool startCard // Indicates if the card is available at the start of the game
    ) : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        // Constructor of the base class (GreenCard) is called with the provided parameters
    }

    // Copy constructor for creating a new instance of GroceryStore by copying an existing instance
    public GroceryStore(GroceryStore copyCard) : base(copyCard)
    {
        // Constructor of the base class (GreenCard) is called with the provided copied card
    }

    // Override method to create a copy of the GroceryStore instance
    public override Establishment Copy()
    {
        return new GroceryStore(this);
    }

    // Override method to perform special actions when the card's effect is triggered
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        // Call the PerformSpecial method of the base class (GreenCard)
        base.PerformSpecial(player, target, players);

        // Iterate through each building in the player's possession
        foreach (var building in player.buildingCards)
        {
            // Iterate through each card in the current building
            foreach (var card in building)
            {
                // Check if the card is of type FARM
                if (card.cardType == CardType.FARM)
                {
                    // Increase the player's coins by the gains specified for the GroceryStore
                    player.coins += gains;
                }
            }
        }
    }
}
