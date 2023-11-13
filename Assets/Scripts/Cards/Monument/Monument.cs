public class Monument : Card
{
    public bool built;

    public Monument(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        this.built = built;
    }
}
