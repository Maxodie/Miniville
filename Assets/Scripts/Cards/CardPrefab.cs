using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
