using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class StartScreenManager
{
    [SerializeField] GameObject playerFrame3;
    [SerializeField] GameObject playerFrame4;
    
    public void UpdatePlayerFrames(UIData uiData, int nbPlayer, Button addBtn, Button removeBtn)
    {
        switch (nbPlayer)
        {
            case 2:
                playerFrame3.SetActive(false);

                removeBtn.GetComponent<Image>().sprite = uiData.grayBtnSprite;
                break;
            case 3:
                playerFrame3.SetActive(true);
                playerFrame4.SetActive(false);

                removeBtn.GetComponent<Image>().sprite = uiData.redBtnSprite;
                addBtn.GetComponent<Image>().sprite = uiData.yellowBtnSprite;
                break;
            case 4:
                playerFrame4.SetActive(true);
                
                addBtn.GetComponent<Image>().sprite = uiData.grayBtnSprite;
                break;
        }
    }

    public void SetupPlayerFrames(UIData uiData, GameData gameData)
    {
        for (int i = 0; i < gameData.players.Length; i++)
        {
            //GameObject activeFrame = uiData.playerFrames
            //gameData.players[i].playerFrame = new PlayerFrame();
        }
    }
}