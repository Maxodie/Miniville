public class Trawler : BlueCard
{
    public Trawler(string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType)
            : base(cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType)
        {
            
        }
    
        public override void PerformSpecial(Player player, Player target)
        {
            // if the player owns a port then the trawler remunerates the player owning it with the value of the dice throw
            foreach (var monument in player.monumentCards)
            {
                if (monument.cardName == "Port")
                    player.coins += player.ThrowDice(2);
            }
        }
}