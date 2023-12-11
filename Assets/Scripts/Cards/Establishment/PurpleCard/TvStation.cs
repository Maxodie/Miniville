public class TvStation : PurpleCard
{
    public TvStation(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);

        if (target.coins <= 5)
        {
            player.coins += target.coins;
            target.coins = 0;
        }
        else
        {
            player.coins += 5;
            target.coins -= 5;
        }
    }
}
