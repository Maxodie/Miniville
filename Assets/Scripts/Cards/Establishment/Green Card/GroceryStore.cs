public class GroceryStore : GreenCard
{
    public GroceryStore(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        
        foreach (var building in player.buildingCards)
        {
            if (building.cardType == CardType.FARM)
                player.coins += gains;
        }
    }
}
