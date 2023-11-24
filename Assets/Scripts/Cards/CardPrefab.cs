using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
public class CardPrefab {
    public Button btn;
    GameObject gameObject;

    public CardPrefab(Card card, GameObject gameObject) {
        LoadGoCard(card, gameObject);
    }

    public void LoadGoCard(Card card, GameObject gameObject) {
        this.gameObject = gameObject;

        for(int i=0; i <gameObject.transform.childCount; i++) {
            Transform child = gameObject.transform.GetChild(i);

            switch (child.tag)
            {
                case "CardImg":
                    child.GetComponent<Image>().sprite = card.cardSprite;
                    break;
                case "TypeIconImg":
                    child.GetComponent<Image>().sprite = card.cardTypeIcon;
                    break;
                case "CardTextName":
                    child.GetComponent<TMP_Text>().text = card.cardName;
                    break;
                case "DescriptionText":
                    child.GetComponent<TMP_Text>().text = card.cardEffectDescription;
                    break;
            }
        }
    }

    public void Destroy() {
        Object.Destroy(gameObject);
    }
}
*/

public class CardPrefab {
    GameObject loadedGo;
    [HideInInspector] public Button loadedBtn;
    public Image cardImg;
    public Image iconType;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public CardPrefab(CardUIData cardUIData, Transform spawnPoint, Card card) {
        loadedGo = Object.Instantiate(cardUIData.objectPrefab, spawnPoint);
        LoadGoCard(cardUIData, card);;
    }

    public void LoadGoCard(CardUIData cardUIData, Card card) {
        loadedBtn = loadedGo.GetComponent<Button>();
        
         for(int i=0; i <loadedGo.transform.childCount; i++) {
            Transform child = loadedGo.transform.GetChild(i);

            if(cardUIData.cardImgStringTag == child.tag)
                child.GetComponent<Image>().sprite = card.cardSprite;

            if(cardUIData.iconTypeStringTag == child.tag)
                child.GetComponent<Image>().sprite = card.cardTypeIcon;
            
            if(cardUIData.cardNameTextStringTag == child.tag)
                child.GetComponent<TMP_Text>().text = card.cardName;

            if(cardUIData.cardDescriptionStringTag == child.tag)
                child.GetComponent<TMP_Text>().text = card.cardEffectDescription;

            if(cardUIData.costCardTag == child.tag)
                child.GetComponent<TMP_Text>().text = card.constructionCost.ToString();
        }
    }

    public void Destroy() {
        Object.Destroy(loadedGo);
    }
}