public class Establishment : Card
{
    public int[] requiredDiceValues;

    public Establishment(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        
    }
}
