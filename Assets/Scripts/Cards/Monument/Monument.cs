public class Monument : Card
{
    public bool built;

    // Constructor for creating a new instance of Monument
    public Monument(
        CardGoPrefab cardBehaviour, // GameObject prefab for the card's graphical representation
        string cardImgPath, // Path to the card's image
        string cardName, // Name of the card
        CardType cardType, // Type of the card
        string cardEffectDescription, // Description of the card's effect
        int constructionCost, // Cost to construct/build the card
        int gains, // Coins gained when the card's effect is triggered
        CardType requiredCardType, // Type of card required for effect
        bool built, // Indicates if the card has been built
        CardPriority cardPriority // Priority of the card
    ) : base(cardBehaviour, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        // Constructor of the base class (Card) is called with the provided parameters
        this.built = built;
    }

    // Copy constructor for creating a new instance of Monument by copying an existing instance
    public Monument(Monument copy) : base(copy)
    {
        // Constructor of the base class (Card) is called with the provided copied card
        this.built = copy.built;
    }
    
    // Virtual method to create a copy of the Monument instance (can be overridden in derived classes)
    public virtual Monument Copy()
    {
        return new Monument(this);
    }

    // Override for the equality operator (==) to compare Monument instances based on their types
    public static bool operator==(Monument a, Monument b)
    {
        return a.GetType() == b.GetType();
    }

    // Override for the inequality operator (!=) to compare Monument instances based on their types
    public static bool operator!=(Monument a, Monument b)
    {
        return a.GetType() != b.GetType();
    }

    // Override method to compare Monument instances for equality
    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    // Override method to generate a hash code for the Monument instance
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
