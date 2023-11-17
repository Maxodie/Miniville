public class AmusementPark : Monument
{
    public AmusementPark(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        
    }

    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        if (player.throwValue1 == player.throwValue2)
        {
            player.canReplay = true;
        }
    }
}
