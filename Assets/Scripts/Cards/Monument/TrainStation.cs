using System.Diagnostics;
using UnityEngine;
public class TrainStation : Monument
{
    public TrainStation(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,
        CardType requiredCardType, bool built, CardPriority cardPriority)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, built, cardPriority)
    {
        
    }

    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        player.currentDice = 2;
        UnityEngine.Debug.Log("tee");
    }
}
