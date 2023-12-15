public class Monument : Card
{
    public bool built;

    public Monument(CardGoPrefab cardBehaviour, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardBehaviour, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        this.built = built;
    }

    public Monument(Monument copy) : base(copy) {
        this.built = copy.built;
    }
    
    public virtual Monument Copy() {
        return new Monument(this);
    }

    public static bool operator==(Monument a, Monument b) {
        return a.GetType() == b.GetType();
    }

    public static bool operator!=(Monument a, Monument b) {
        return a.GetType() != b.GetType();
    }

    public override bool Equals(object obj) {
        return base.Equals(obj);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }
}
