using UnityEngine;

[System.Serializable]
public class UIData
{
    [Header("Button Sprites")] [SerializeField] 
    public Sprite grayBtnSprite; 
    public Sprite redBtnSprite;
    public Sprite yellowBtnSprite;
    public Sprite blueBtnSprite;

    [Header("Player ui data")] [SerializeField]
    public GameObject[] playerFrames;
}
