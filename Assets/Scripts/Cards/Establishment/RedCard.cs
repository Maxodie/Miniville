public class RedCard : Establishment
{
    public RedCard(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target)
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
