public class Monument : Card
{
    public bool built;

    public Monument(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        this.built = built;
    }

    public static bool operator==(Monument a, Monument b) {
        return a.GetType() == b.GetType();
    }

    public static bool operator!=(Monument a, Monument b) {
        return a.GetType() != b.GetType();
    }
}
