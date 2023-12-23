public class Establishment : Card
{
    // Array to store required dice values for the card's effect
    public int[] requiredDiceValues;

    // Indicates if the card is available at the start of the game
    public bool startCard;

    // Constructor for creating a new instance of Establishment
    public Establishment(
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
    ) : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        // Initialize the requiredDiceValues and startCard properties
        this.requiredDiceValues = requiredDiceValues;
        this.startCard = startCard;
    }

    // Copy constructor for creating a new instance of Establishment by copying an existing instance
    public Establishment(Establishment copy) : base(copy)
    {
        // Copy the requiredDiceValues and startCard properties from the provided card
        requiredDiceValues = copy.requiredDiceValues;
        startCard = copy.startCard;
    }

    // Method to check if the card's effect can be performed with the given dice result
    public bool canPerformEffect(int diceResult)
    {
        // Iterate through the requiredDiceValues to check if the provided diceResult is valid
        for (int i = 0; i < requiredDiceValues.Length; i++)
        {
            if (requiredDiceValues[i] == diceResult)
                return true;
        }

        // If no match is found, the card's effect cannot be performed
        return false;
    }

    // Virtual method to create a copy of the Establishment instance (can be overridden in derived classes)
    public virtual Establishment Copy()
    {
        return new Establishment(this);
    }
}
