public class BusinessCenter : PurpleCard
{
    public BusinessCenter(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        
    }
}
