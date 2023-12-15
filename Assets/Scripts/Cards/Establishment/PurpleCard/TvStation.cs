public class TvStation : PurpleCard
{
    public TvStation(
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
        // Constructor of the base class (PurpleCard) is called with the provided parameters
    }

    // Copy constructor for creating a new instance of TvStation by copying an existing instance
    public TvStation(TvStation copyCard) : base(copyCard)
    {
        // Constructor of the base class (PurpleCard) is called with the provided copied card
    }

    // Override method to create a copy of the TvStation instance
    public override Establishment Copy()
    {
        return new TvStation(this);
    }

    // Override method to perform special actions when the card's effect is triggered
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        // Call the PerformSpecial method of the base class (PurpleCard)
        base.PerformSpecial(player, target, players);

        // Check if the target player has 5 or fewer coins
        if (target.coins <= 5)
        {
            // Transfer all coins from the target player to the owner of the TvStation
            player.AddCoin(target.coins);
            target.coins = 0;
        }
        else
        {
            // Transfer 5 coins from the target player to the owner of the TvStation
            player.AddCoin(5);
            target.AddCoin(-5);
        }
    }
}
