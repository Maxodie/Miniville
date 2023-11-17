using UnityEngine;

public class Card
{
    public Sprite cardSprite;
    public string cardName;
    public CardType cardType;
    public Sprite cardTypeIcon;
    public string cardEffectDescription;
    public int constructionCost;
    public int gains;
    public CardType requiredCardType;
    public CardPriority cardPriority;

    public Card(string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,CardType requiredCardType, CardPriority cardPriority)
    {
        Debug.Log(cardImgPath);
        this.cardSprite = Resources.Load<Sprite>(cardImgPath);
        this.cardName = cardName; 
        this.cardType = cardType;
        this.cardEffectDescription = cardEffectDescription;
        this.constructionCost = constructionCost;
        this.gains = gains;
        this.requiredCardType = requiredCardType;
        this.cardPriority = cardPriority;

        LoadCardTypeIcon();
    }

    void LoadCardTypeIcon() {
        cardTypeIcon = Resources.Load<Sprite>($"CardTypeImg/{cardType}");
    }

    public virtual void PerformSpecial(Player player, Player target, Player[] players)
    {
        
    }
}
