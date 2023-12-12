public class Stadium : PurpleCard
{
    public Stadium(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }

    public Stadium(Stadium copyCard) : base(copyCard) {

    }

    public override Establishment Copy() {
        return new Stadium(this);
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        base.PerformSpecial(player, target, players);
        
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != player)
            {
                if (players[i].coins >= 2)
                {
                    players[i].AddCoin(-2);
                    player.AddCoin(2);
                }
                else
                {
                    player.AddCoin(players[i].coins);
                    players[i].coins = 0;

                }
            }
        }
    }
}
