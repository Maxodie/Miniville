public class RedCard : Establishment
{
    public RedCard(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        if (target.coins <= gains)
        {
            target.coins -= target.coins;
        }
        else
        {
            target.coins -= gains;
        }

        player.coins += gains;
    }
}
