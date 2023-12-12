public class CheeseFactory : GreenCard
{
    public CheeseFactory(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }

    public CheeseFactory(CheeseFactory copyCard) : base(copyCard) {

    }

    public override Establishment Copy() {
        return new CheeseFactory(this);
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        
        foreach (var building in player.buildingCards)
        {
            foreach (var card in building)
            {
                if (card.cardType == CardType.FARM)
                    player.coins += gains;
            }
        }
    }
}
