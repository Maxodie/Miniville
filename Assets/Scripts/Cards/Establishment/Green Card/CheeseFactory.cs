public class CheeseFactory : GreenCard
{
    public CheeseFactory(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target)
    {
        foreach (var building in player.buildingCards)
        {
            if (building.cardType == CardType.FARM)
                player.coins += gains;
        }
    }
}
