public class Stadium : PurpleCard
{
    public Stadium(
        CardGoPrefab cardGoPrefab, // GameObject prefab for the card's graphical representation
        string cardImgPath, // Path to the card's image
        string cardName, // Name of the card
        CardType cardType, // Type of the card
        string cardEffectDescription, // Description of the card's effect
        int constructionCost, // Cost to construct/build the card
        int gains, // Coins gained when the card's effect is triggered
        CardType requiredCardType, // Type of card required for effect
        CardPriority cardPriority, // Priority of the card
        int[] requiredDiceValues, // Array of dice values required for construction
        bool startCard // Indicates if the card is available at the start of the game
    ) : base(cardGoPrefab, cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority, requiredDiceValues, startCard)
    {
        // Constructor of the base class (PurpleCard) is called with the provided parameters
    }

    // Copy constructor for creating a new instance of Stadium by copying an existing instance
    public Stadium(Stadium copyCard) : base(copyCard)
    {
        // Constructor of the base class (PurpleCard) is called with the provided copied card
    }

    // Override method to create a copy of the Stadium instance
    public override Establishment Copy()
    {
        return new Stadium(this);
    }

    // Override method to perform special actions when the card's effect is triggered
    public override void PerformSpecial(Player player, Player target, Player[] players)
    {
        // Call the PerformSpecial method of the base class (PurpleCard)
        base.PerformSpecial(player, target, players);

        // Iterate through each player in the game
        for (int i = 0; i < players.Length; i++)
        {
            // Check if the current player is not the owner of th Stadium card
            if (players[i] != player)
            {
                // Check if the current player has at least 2 coins
                if (players[i].coins >= 2)
                {
                    // Transfer 2 coins from the current player to the owner of the Stadium
                    players[i].AddCoin(-2);
                    player.AddCoin(2);
                }
                else
                {
                    // Transfer all coins from the current player to the owner of the Stadium
                    player.AddCoin(players[i].coins);
                    players[i].coins = 0;
                }
            }
        }
    }
}
