public class RedCard : Establishment
{
    public RedCard(
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
        // Constructor of the base class (Establishment) is called with the provided parameters
    }

    // Copy constructor for creating a new instance of RedCard by copying an existing instance
    public RedCard(RedCard copyCard) : base(copyCard)
    {
        // Constructor of the base class (Establishment) is called with the provided copied card
    }

    // Override method to create a copy of the RedCard instance
    public override Establishment Copy()
    {
        return new RedCard(this);
    }

    // Override method to perform special actions when the card's effect is triggered
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        // Call the PerformSpecial method of the base class (Establishment)
        base.PerformSpecial(player, target, players);

        // Check if the target player has fewer coins than or equal to the gains specified for the RedCard
        if (target.coins <= gains)
        {
            // Set the target player's coins to zero
            target.coins = 0;
        }
        else
        {
            // Decrease the target player's coins by the specified gains for the RedCard
            target.coins -= gains;
        }

        // Increase the owner's (player's) coins by the specified gains for the RedCard
        player.coins += gains;
    }
}
