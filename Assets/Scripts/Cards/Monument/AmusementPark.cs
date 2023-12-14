public class AmusementPark : Monument
{
    public AmusementPark(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        
    }

    public AmusementPark(AmusementPark copy) : base(copy) {

    }

    public override Monument Copy() {
        return new AmusementPark(this);
    }

    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        
        if(player.throwValue.Length > 1) {
            if (player.throwValue[0] == player.throwValue[1])
            {
                player.canReplay = true;
            }
        }
    }
}
