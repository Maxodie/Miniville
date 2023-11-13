public class ShoppingMall : Monument
{
    public ShoppingMall(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built)
    {
        
    }

    public override void PerformSpecial(Player player, Player target)
    {
        foreach (var building in player.buildingCards)
        {
            if (building.cardType == CardType.BUSINESS || building.cardType == CardType.GROCERYSTORE)
                building.gains++;
        }
    }
}
