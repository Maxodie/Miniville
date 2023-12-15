using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class CardUIPrefab {
    // GameObject for the card UI
    public GameObject loadedGo;
    // Button component of the card UI
    [HideInInspector] public Button loadedBtn;
    // Sprite for the card
    public Sprite cardSprite;
    // Image for the card type icon
    public Image iconType;
    // Text for the card name
    public TMP_Text cardName;
    // Text for the card description
    public TMP_Text cardDescription;
    // EventTrigger for handling hover events
    EventTrigger hoverEventCo;
    // Reference to MonoBehaviour
    MonoBehaviour monoBehaviour;
    // Reference to BuildBehaviour (optional)
    BuildBehaviour buildBehaviour;
    // Reference to InteractionBehaviour (optional)
    InteractionBehaviour interactionBehaviour;
    
    // Constructor to initialize the CardUIPrefab
    public CardUIPrefab(CardUIData cardUIData, Transform spawnPoint, Card card, MonoBehaviour monoBehaviour, BuildBehaviour buildBehaviour = null, InteractionBehaviour interactionBehaviour = null) {
        loadedGo = Object.Instantiate(cardUIData.objectPrefab, spawnPoint);
        this.monoBehaviour = monoBehaviour;
        this.buildBehaviour = buildBehaviour;
        this.interactionBehaviour = interactionBehaviour;
        cardSprite = card.cardSprite;
        LoadGoCard(cardUIData, card);
    }

    // Method to load the visual elements of the card UI
    public void LoadGoCard(CardUIData cardUIData, Card card) {
        loadedBtn = loadedGo.GetComponent<Button>();
        loadedBtn.image.sprite = card.cardSprite;
        loadedBtn.onClick.AddListener(() => monoBehaviour.StopAllCoroutines());

        // Iterate through children of the loaded GameObject and assign sprites/text based on their tags
        for (int i = 0; i < loadedGo.transform.childCount; i++) {
            Transform child = loadedGo.transform.GetChild(i);

            // Set card image
            if (cardUIData.cardImgStringTag == child.tag)
                child.GetComponent<Image>().sprite = card.cardSprite;

            // Set icon type image
            if (cardUIData.iconTypeStringTag == child.tag)
                child.GetComponent<Image>().sprite = card.cardTypeIcon;

            // Set card name text
            if (cardUIData.cardNameTextStringTag == child.tag)
                child.GetComponent<TMP_Text>().text = card.cardName;

            // Set card description text
            if (cardUIData.cardDescriptionStringTag == child.tag)
                child.GetComponent<TMP_Text>().text = card.cardEffectDescription;

            // Set card cost text
            if (cardUIData.costCardTag == child.tag)
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

    // Method to destroy the loaded GameObject
    public void Destroy() {
        Object.Destroy(loadedGo);
    }

    // Method to start displaying card information on hover
    void StartDisplayCardRoutine(BaseEventData eventData) {
        monoBehaviour.StartCoroutine(DisplayCard());
    }

    // Method to stop displaying card information on hover exit
    void StopDisplayCardRoutine(BaseEventData eventData) {
        monoBehaviour.StopAllCoroutines();

        // Hide up-sized card when hovering stops
        buildBehaviour?.DisplayUpSizedCard(false);
        interactionBehaviour?.DisplayUpSizedCard(false);
    }

    // Coroutine to display card information after a delay
    IEnumerator DisplayCard() {
        yield return new WaitForSeconds(1);
        // Show up-sized card with the card sprite
        buildBehaviour?.DisplayUpSizedCard(true, cardSprite);
        interactionBehaviour?.DisplayUpSizedCard(true, cardSprite);
    }
}
