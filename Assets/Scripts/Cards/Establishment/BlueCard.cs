public class BlueCard : Establishment
{
    public BlueCard(
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

    // Copy constructor for creating a new instance of BlueCard by copying an existing instance
    public BlueCard(BlueCard copyCard) : base(copyCard)
    {
        // Constructor of the base class (Establishment) is called with the provided copied card
    }

    // Override method to create a copy of the BlueCard instance
    public override Establishment Copy()
    {
        return new BlueCard(this);
    }

    // Override method to perform special actions when the card's effect is triggered
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        // Call the PerformSpecial method of the base class (Establishment)
        base.PerformSpecial(player, target, players);

        // Increase the owner's coins by the specified gains for the BlueCard
        player.AddCoin(gains);
    }
}