using TMPro;
using UnityEngine;

public class PlayerFrame
{
    public Player player;
    public GameObject activeFrame;
    public GameObject defaultFrame;
    public TMP_Text nameText;
    public TMP_Text nbMonumentText;
    public TMP_Text nbCoinText;

    public PlayerFrame(Player player, GameObject defaultFrame, string nameText, UIPlayerFrameScriptableObject uIPlayerFrameScriptableObject)
    {
        this.defaultFrame = defaultFrame;
        this.player = player;

        for(int i = 0; i < defaultFrame.transform.childCount; i++) {
            Transform tr = defaultFrame.transform.GetChild(i);

            ReferenceChild(tr, uIPlayerFrameScriptableObject);  
        }

        this.nameText.text = nameText;
        UpdateUI();
    }

    void ReferenceChild(Transform tr, UIPlayerFrameScriptableObject uIPlayerFrameScriptableObject) {
        if(tr.tag == uIPlayerFrameScriptableObject.nameText) {
            this.nameText = tr.GetComponent<TMP_Text>();
        }
        else if(tr.tag == uIPlayerFrameScriptableObject.nbMonumentText) {
            nbMonumentText = tr.GetComponent<TMP_Text>();
        }
        else if(tr.tag == uIPlayerFrameScriptableObject.nbCoinText) {
            nbCoinText = tr.GetComponent<TMP_Text>();
        }
        else if(tr.tag == uIPlayerFrameScriptableObject.activeFrame) {
            activeFrame = tr.gameObject;
        }
    }

    public void UpdateUI() {

        this.nbMonumentText.text = player.GetMonumentBuilt().ToString();
        this.nbCoinText.text = player.coins.ToString();
    }

    public void SetCurrentActiveFrame(bool state) {
        activeFrame.SetActive(state);
    }
}