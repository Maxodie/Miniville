using UnityEngine.UI;
using UnityEngine;

[System.Serializable]
public class StartScreenManager
{
    [SerializeField] ButtonImage[] startButtons;

    public void SelectButton() {
        
    }
}

[System.Serializable]
public class ButtonImage {
    [SerializeField] Button btn;
    [SerializeField] Image leftImg;
    [SerializeField] Image rightImg;
}