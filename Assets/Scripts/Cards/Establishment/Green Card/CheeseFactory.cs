public class CheeseFactory : GreenCard
{
    public CheeseFactory(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
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
