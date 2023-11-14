public class TvStation : PurpleCard
{
    public TvStation(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target)
    {
        
    }
}
