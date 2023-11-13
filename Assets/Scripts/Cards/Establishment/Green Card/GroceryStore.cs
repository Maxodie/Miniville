public class GroceryStore : GreenCard
{
    public GroceryStore(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target)
    {
        foreach (var building in player.buildingCards)
        {
            if (building.cardType == CardType.ANIMALFARM)
                player.coins += gains;
        }
    }
}
