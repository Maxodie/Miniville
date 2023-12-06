public class ShoppingMall : Monument
{
    public ShoppingMall(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        
    }

    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        
        foreach (var building in player.buildingCards)
        {
            foreach (var card in building)
            {
                if (card.cardType == CardType.BUSINESS || card.cardType == CardType.GROCERYSTORE)
                    card.gains++;
            }
        }
    }
}
