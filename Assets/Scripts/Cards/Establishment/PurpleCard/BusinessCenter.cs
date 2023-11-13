public class BusinessCenter : PurpleCard
{
    public BusinessCenter(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target)
    {
        
    }
}
