using TMPro;
using UnityEngine;

public class PlayerFrame
{
    public GameObject activeFrame;
    public TextMeshPro nameText;
    public TextMeshPro nbMonumentText;
    public TextMeshPro nbCoinText;

    public PlayerFrame(GameObject activeFrame, TextMeshPro nameText, TextMeshPro nbMonumentText, TextMeshPro nbCoinText)
    {
        this.activeFrame = activeFrame;
        this.nameText = nameText;
        this.nbMonumentText = nbMonumentText;
        this.nbCoinText = nbCoinText;
    }
}