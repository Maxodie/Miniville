public class BusinessCenter : PurpleCard
{
    public BusinessCenter(
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

    // Copy constructor for creating a new instance of BusinessCenter by copying an existing instance
    public BusinessCenter(BusinessCenter copyCard) : base(copyCard)
    {
        // Constructor of the base class (PurpleCard) is called with the provided copied card
    }

    // Override method to create a copy of the BusinessCenter instance
    public override Establishment Copy()
    {
        return new BusinessCenter(this);
    }
}