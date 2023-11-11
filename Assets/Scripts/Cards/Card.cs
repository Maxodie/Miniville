public class Card
{
    public string cardName;
    public enum cardType{}
    public string cardEffectDescription;
    public int constructionCost;
    public int gains;
    public enum requiredCardType{}

    public Card(string cardName, string cardEffectDescription, int constructionCost, int gains)
    {
        this.cardName = cardName;
        this.cardEffectDescription = cardEffectDescription;
        this.constructionCost = constructionCost;
        this.gains = gains;
    }

    public virtual void PerformSpecial(Player player, Player target)
    {
        
    }
}
