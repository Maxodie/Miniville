using UnityEngine;
public class TrainStation : Monument
{
    public TrainStation(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        
    }

    public TrainStation(TrainStation copy) : base(copy) {
        
    }

    public override Monument Copy() {
        return new TrainStation(this);
    }


    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        player.currentDice = 2;
    }
}
