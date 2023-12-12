using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUIPrefab {
    public GameObject loadedGo;
    [HideInInspector] public Button loadedBtn;
    public Image cardImg;
    public Image iconType;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    EventTrigger hoverEventCo;
    MonoBehaviour monoBehaviour;
    
    public CardUIPrefab(CardUIData cardUIData, Transform spawnPoint, Card card, MonoBehaviour monoBehaviour) {
        loadedGo = Object.Instantiate(cardUIData.objectPrefab, spawnPoint);
        this.monoBehaviour = monoBehaviour;
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

        hoverEventCo = loadedGo.GetComponent<EventTrigger>();
         
        // Set on pointer enter event listener
        EventTrigger.Entry hoverEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerEnter
        };
        hoverEvent.callback.AddListener(StartDisplayCardRoutine);
        hoverEventCo.triggers.Add(hoverEvent);
        
        // Set on pointer exit event listener
        EventTrigger.Entry hoverExitEvent = new EventTrigger.Entry()
        {
            eventID = EventTriggerType.PointerExit
        };
        hoverExitEvent.callback.AddListener(StartDisplayCardRoutine);
        hoverEventCo.triggers.Add(hoverExitEvent);
        
    }

    public void Destroy() {
        Object.Destroy(loadedGo);
    }

    void StartDisplayCardRoutine(BaseEventData eventData)
    {
        Debug.Log("Hey ! Listen");
        monoBehaviour.StartCoroutine(DisplayCard());
    }

    void StopDisplayCardRoutine(BaseEventData eventData)
    {
        Debug.Log("OK OK JE STOP MA TRUC");
        monoBehaviour.StopCoroutine(DisplayCard());
    }

    IEnumerator DisplayCard()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("OUI QUESQUE TU VEU ?");
    }
}