using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    public Image imgBg,imgIcon,imgProcess;
    public TextMeshProUGUI txtProcess,txtLevel;
    public InfoCollectionPlayer infoCollectionPlayer;
    [SerializeField]
    Sprite sprBarNormal, sprBarCanUpgrade;
    [SerializeField] GameObject iconUp;
    private void OnEnable()
    {
        Texture2D texture = Resources.Load<Texture2D>("Collection/" + LoadCollectionData.ins.GetColection(infoCollectionPlayer.idBonus, infoCollectionPlayer.rare).nameIcon);
        imgIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        imgIcon.SetNativeSize();
        UpdateItem();
    }

    public void UpdateItem()
    {
        imgProcess.fillAmount = Mathf.Min(infoCollectionPlayer.amount / Mathf.Pow(2, infoCollectionPlayer.level - 1), 1);
        txtProcess.text = infoCollectionPlayer.amount + "/" + Mathf.Pow(2, infoCollectionPlayer.level - 1);
        txtLevel.text = "Level " + infoCollectionPlayer.level;
        if (infoCollectionPlayer.amount >= Mathf.Pow(2, infoCollectionPlayer.level - 1))
        {
            imgProcess.sprite = sprBarCanUpgrade;
            iconUp.SetActive(true);
        }
        else
        {
            imgProcess.sprite = sprBarNormal;
            iconUp.SetActive(false);
        }
    }
    public void OnShowDetail()
    {
        PanelCollectionController.ins.ShowDetail(this);
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);

    }

}
