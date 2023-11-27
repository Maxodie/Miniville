using UnityEngine;

public class Card
{
    public CardGoPrefab cardGoPrefab;
    public GameObject spawnedGoCard;
    GameObject spawnedGoBuilding;
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

    void LoadCardTypeIcon() {
        cardTypeIcon = Resources.Load<Sprite>($"CardTypeImg/{cardType}");
    }

    public virtual void PerformSpecial(Player player, Player target, Player[] players)
    {
        
    }

    public void InstantiateCard(Vector3 pos, Quaternion rot, bool activeBuilding) {
        spawnedGoCard = Object.Instantiate(cardGoPrefab.cardGo, pos, rot);

        if(activeBuilding)
            InstantiateBuilding(pos, rot);
    }

    public void InstantiateBuilding(Vector3 pos, Quaternion rot) {
        spawnedGoBuilding = Object.Instantiate(cardGoPrefab.buildingGo, pos, rot);
    }
}
