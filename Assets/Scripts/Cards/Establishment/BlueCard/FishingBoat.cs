public class FishingBoat : BlueCard
{
    public FishingBoat(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType)
        : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType)
    {
            
    }
    
    public override void PerformSpecial(Player player, Player target)
    {
        foreach (var monument in player.monumentCards)
        {
            if (monument.cardName == "Port")
                player.coins += gains;
        }
    }
}