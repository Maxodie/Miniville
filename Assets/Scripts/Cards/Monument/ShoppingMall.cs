public class ShoppingMall : Monument
{
    public ShoppingMall(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        
    }

    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        foreach (var building in player.buildingCards)
        {
            if (building.cardType == CardType.BUSINESS || building.cardType == CardType.GROCERYSTORE)
                building.gains++;
        }
    }
}
