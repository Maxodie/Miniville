using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardUIPrefab {
    public GameObject loadedGo;
    [HideInInspector] public Button loadedBtn;
    public Image cardImg;
    public Image iconType;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    public CardUIPrefab(CardUIData cardUIData, Transform spawnPoint, Card card) {
        loadedGo = Object.Instantiate(cardUIData.objectPrefab, spawnPoint);
        LoadGoCard(cardUIData, card);
    }

    public void LoadGoCard(CardUIData cardUIData, Card card) {
        loadedBtn = loadedGo.GetComponent<Button>();
        loadedBtn.image.sprite = card.cardSprite;
        
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