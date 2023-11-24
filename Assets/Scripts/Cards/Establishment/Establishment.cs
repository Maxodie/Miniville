public class Establishment : Card
{
    public int[] requiredDiceValues;
    public bool startCard;

    public Establishment(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains, CardType requiredCardType, CardPriority cardPriority, int[] requiredDiceValues, bool startCard)
        : base(cardImgPath, cardName, cardType, cardEffectDescription, constructionCost, gains, requiredCardType, cardPriority)
    {
        this.requiredDiceValues = requiredDiceValues;
        this.startCard = startCard;
    }

    public bool canPerformEffect(int diceResult) {
        for(int i=0; i < requiredDiceValues.Length; i++) {
            if(requiredDiceValues[i] == diceResult)
                return true;
        }

        return false;
    }
}
