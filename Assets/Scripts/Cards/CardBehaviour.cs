using UnityEngine;

public class CardBehaviour {
    public Card card;
    public CardGoPrefab cardGoPrefab;
    public GameObject spawnedGoCard;
    Animator cardAnim;
    public GameObject spawnedGoBuilding;

    public CardBehaviour(Card card, CardGoPrefab cardGoPrefab) {
        this.card = card;
        this.cardGoPrefab = cardGoPrefab;
    }

    public void InstantiateCard(Transform tr, Vector3 pos, bool activeBuilding) {
        spawnedGoCard = Object.Instantiate(cardGoPrefab.cardGo, tr);
        spawnedGoCard.transform.localPosition = pos;
        cardAnim = spawnedGoCard.GetComponent<Animator>();

        if(activeBuilding)
            InstantiateBuilding(tr, pos);

    }

    public void InstantiateBuilding(Transform tr, Vector3 pos) {
        spawnedGoBuilding = Object.Instantiate(cardGoPrefab.buildingGo, tr);
        spawnedGoBuilding.transform.localPosition = pos;
    }

    public void PerformVisualEffect() {
        if(!cardAnim)
            Debug.Log("pt n de sa mere");
        else
            cardAnim.SetTrigger("ActiveEffect");
    }
}
