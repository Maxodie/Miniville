public class Card
{
    public string cardName;
    public CardType cardType;
    public string cardEffectDescription;
    public int constructionCost;
    public int gains;
    public CardType requiredCardType;
    public CardPriority cardPriority;

    public Card(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,CardType requiredCardType, CardPriority cardPriority)
    {
        this.cardName = cardName;
        this.cardType = cardType;
        this.cardEffectDescription = cardEffectDescription;
        this.constructionCost = constructionCost;
        this.gains = gains;
        this.requiredCardType = requiredCardType;
        this.cardPriority = cardPriority;
    }

    public virtual void PerformSpecial(Player player, Player target)
    {
        
    }
}
