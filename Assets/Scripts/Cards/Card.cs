using System;
using UnityEngine;

public class Card
{
    public CardBehaviour cardBehaviour;
    public CardGoPrefab cardGoPrefab;
    public Sprite cardSprite;
    public string cardName;
    public CardType cardType;
    public Sprite cardTypeIcon;
    public string cardEffectDescription;
    public int constructionCost;
    public int gains;
    public CardType requiredCardType;
    public CardPriority cardPriority;

    public Card(CardGoPrefab cardGoPrefab, string cardImgPath, string cardName, CardType cardType, string cardEffectDescription, int constructionCost, int gains,CardType requiredCardType, CardPriority cardPriority)
    {
        this.cardGoPrefab = cardGoPrefab;
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

    protected Card(Card copyCard) {
        this.cardGoPrefab = copyCard.cardGoPrefab;

        this.cardBehaviour = copyCard.cardBehaviour;
        this.cardSprite = copyCard.cardSprite;
        this.cardName = copyCard.cardName; 
        this.cardType = copyCard.cardType;
        this.cardEffectDescription = copyCard.cardEffectDescription;
        this.constructionCost = copyCard.constructionCost;
        this.gains = copyCard.gains;
        this.requiredCardType = copyCard.requiredCardType;
        this.cardPriority = copyCard.cardPriority;
    }

    public Card Copy() {
        return new Card(this);
    }

    public void CreateNewCardBehaviour() {
        cardBehaviour = new CardBehaviour(this, cardGoPrefab);
    }

    void LoadCardTypeIcon() {
        cardTypeIcon = Resources.Load<Sprite>($"CardTypeImg/{cardType}");
    }

    public virtual void PerformSpecial(Player player, Player target, Player[] players)
    {
        cardBehaviour.PerformVisualEffect();
    }
}