public class GroceryStore : GreenCard
{
    public GroceryStore(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        foreach (var building in player.buildingCards)
        {
            if (building.cardType == CardType.FARM)
                player.coins += gains;
        }
    }
}
