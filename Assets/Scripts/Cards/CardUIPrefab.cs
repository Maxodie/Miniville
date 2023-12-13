using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUIPrefab {
    public GameObject loadedGo;
    [HideInInspector] public Button loadedBtn;
    public Sprite cardSprite;
    public Image iconType;
    public TMP_Text cardName;
    public TMP_Text cardDescription;
    EventTrigger hoverEventCo;
    MonoBehaviour monoBehaviour;
    BuildBehaviour buildBehaviour;
    InteractionBehaviour interactionBehaviour;
    
    public CardUIPrefab(CardUIData cardUIData, Transform spawnPoint, Card card, MonoBehaviour monoBehaviour, BuildBehaviour buildBehaviour = null, InteractionBehaviour interactionBehaviour = null) {
        loadedGo = Object.Instantiate(cardUIData.objectPrefab, spawnPoint);
        this.monoBehaviour = monoBehaviour;
        this.buildBehaviour = buildBehaviour;
        this.interactionBehaviour = interactionBehaviour;
        cardSprite = card.cardSprite;
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
        hoverExitEvent.callback.AddListener(StopDisplayCardRoutine);
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
        monoBehaviour.StopAllCoroutines();
        
        buildBehaviour?.DisplayUpSizedCard(false);
        interactionBehaviour?.DisplayUpSizedCard(false);
    }

    IEnumerator DisplayCard()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("OUI QUESQUE TU VEU ?");
        
        buildBehaviour?.DisplayUpSizedCard(true, cardSprite);
        interactionBehaviour?.DisplayUpSizedCard(true, cardSprite);
    }
}