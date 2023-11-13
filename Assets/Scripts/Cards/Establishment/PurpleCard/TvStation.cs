using Mono.Cecil;

public class TvStation : PurpleCard
{
    public TvStation(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target)
    {
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
