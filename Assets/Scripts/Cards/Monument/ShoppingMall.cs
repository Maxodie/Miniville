public class ShoppingMall : Monument
{
    public ShoppingMall(
        CardGoPrefab cardGoPrefab, // GameObject prefab for the card's graphical representation
        string cardImgPath, // Path to the card's image
        string cardName, // Name of the card
        CardType cardType, // Type of the card
        string cardEffectDescription, // Description of the card's effect
        int constructionCost, // Cost to construct/build the card
        int gains, // Coins gained when the card's effect is triggered
        CardType requiredCardType, // Type of card required for effect
        bool built, // Indicates if the card has been built
        CardPriority cardPriority // Priority of the card
    ) : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        // Constructor of the base class (Monument) is called with the provided parameters
    }

    // Copy constructor for creating a new instance of ShoppingMall by copying an existing instance
    public ShoppingMall(ShoppingMall copy) : base(copy)
    {
        // Constructor of the base class (Monument) is called with the provided copied card
    }

    // Override method to create a copy of the ShoppingMall instance
    public override Monument Copy()
    {
        return new ShoppingMall(this);
    }

    // Override method to perform special actions when the card's effect is triggered
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        // Call the PerformSpecial method of the base class (Monument)
        base.PerformSpecial(player, target, players);

        // Iterate through the player's building cards and their associated cards
        foreach (var building in player.buildingCards)
        {
            foreach (var card in building)
            {
                // Check if the card is of type BUSINESS or GROCERYSTORE
                if (card.cardType == CardType.BUSINESS || card.cardType == CardType.GROCERYSTORE)
                {
                    // Increment the gains of the card
                    card.gains++;
                }
            }
        }
    }
}
