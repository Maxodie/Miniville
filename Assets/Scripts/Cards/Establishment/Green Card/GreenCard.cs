using UnityEngine;
public class GreenCard : Establishment
{
    public GreenCard(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }

    public GreenCard(GreenCard copyCard) : base(copyCard) {

    }

    public override Establishment Copy() {
        return new GreenCard(this);
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        player.coins += gains;
    }
}