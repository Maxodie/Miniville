using UnityEngine;

[CreateAssetMenu(fileName = "CardUI", menuName = "Minivilles/CardUI")]
public class CardUIData : ScriptableObject {
    public GameObject objectPrefab;

    public string cardImgStringTag;
    public string iconTypeStringTag;
    public string cardNameTextStringTag;
    public string cardDescriptionStringTag;
    public string costCardTag;
}