public class Stadium : PurpleCard
{
    public Stadium(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        
    }
    
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i] != player)
            {
                if (players[i].coins >= 2)
                {
                    players[i].coins -= 2;
                    player.coins += 2;
                }
                else
                {
                    player.coins += players[i].coins;
                    players[i].coins = 0;

                }
            }
        }
    }
}
