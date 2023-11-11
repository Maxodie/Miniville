public class Establishement : Card
{
    public int[] requiredDiceValues;

    public Establishement(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType)
    {
        
    }
}
