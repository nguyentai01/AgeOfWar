using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelCollectionController : MonoBehaviour
{
    public static PanelCollectionController ins;
    [SerializeField]
    private Transform parentCollection;
    [SerializeField]
    private GameObject prfCollection;
    [SerializeField]
    private Sprite[] sprBg;

    [SerializeField]
    GameObject panelDetail;
    [SerializeField]
    private Image imgBg,imgIconDetail,imgBarProcess;
    [SerializeField]
    private TextMeshProUGUI txtProcess, txtLevel,txtDetail,txtNameCollection;
    [SerializeField] GameObject iconUp;
    [SerializeField]
    private Button btnUpgrade;
    [SerializeField]
    Sprite sprBarNormal, sprBarCanUpgrade;
    ItemCollection itemCollectionSelected;
    [SerializeField]
    GameObject panelGacha,itemGacha;
    [SerializeField]
    Transform parentGacha;
    [SerializeField]
    Button btnGachax1, btnGachax10;
    [SerializeField] GameObject panelTotalBooster;
    [SerializeField] TextMeshProUGUI txtTotalDamage, txtTotalHealth, txtTotalFood, txtTotalCoin;
    private void Start()
    {
        ins = this;
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        LoadCollectionPlayer();
        UpdateBtnGacha();
    }

    void LoadCollectionPlayer()
    {
        foreach(Transform child in parentCollection)
        {
            Destroy(child.gameObject);
        }
        if (LoadCollectionData.ins.dataCollectionPlayer.infoCollectionPlayers == null) return;
        for (int i = 0; i < LoadCollectionData.ins.dataCollectionPlayer.infoCollectionPlayers.Count; i++)
        {
            InfoCollectionPlayer infoCollectionPlayer = LoadCollectionData.ins.dataCollectionPlayer.infoCollectionPlayers[i];
            ItemCollection item = Instantiate(prfCollection, parentCollection).GetComponent<ItemCollection>();
            item.infoCollectionPlayer = infoCollectionPlayer;
            item.imgBg.sprite = sprBg[infoCollectionPlayer.rare - 1];
            item.gameObject.SetActive(true);
        }
    }

    public void EventGacha(int _numberCollection)
    {

        if (_numberCollection == 1)
        {
            Pref.SetRuby(-100);
        }
        else
        {
            Pref.SetRuby(-900);
        }
        foreach(Transform child in parentGacha)
        {
            Destroy(child.gameObject);
        }
        panelGacha.SetActive(true);
        for (int i = 0; i < _numberCollection; i++)
        {
            int idCollection = Random.Range(0, 4);
            Colection newColection = GetColection(idCollection);
            if (newColection != null)
            {
                ItemCollectionGacha item= Instantiate(itemGacha, parentGacha).GetComponent<ItemCollectionGacha>();
                Texture2D texture = Resources.Load<Texture2D>("Collection/" + newColection.nameIcon);
                item.imgIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                item.imgIcon.SetNativeSize();
                item.imgBg.sprite = sprBg[newColection.rare - 1];
                LoadCollectionData.ins.UpdateCollectionPlayer(newColection, idCollection);
            }
        }
        UiManger.Ins.GetRuby();
        UiManger.Ins.SetHeaderFooter(false);

        UpdateBtnGacha();
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);

    }

    Colection GetColection(int _typeCollection)
    {
        // Tạo một số ngẫu nhiên từ 0 đến tổng tỉ lệ
        int randomNumber = Random.Range(0, 100);
        foreach (var pair in LoadCollectionData.ins.dataCollection[_typeCollection].collectionData)
        {
            if (randomNumber < pair.rate)
            {
                return (pair);
                break;
            }
            randomNumber -= pair.rate;
        }
        return null;
    }
    string strDetail;

    public void ShowDetail(ItemCollection _itemCollection)
    {
        panelDetail.SetActive(true);
        itemCollectionSelected = _itemCollection;
        imgBg.sprite= sprBg[_itemCollection.infoCollectionPlayer.rare - 1];
        Texture2D texture = Resources.Load<Texture2D>("Collection/" + LoadCollectionData.ins.GetColection(_itemCollection.infoCollectionPlayer.idBonus, _itemCollection.infoCollectionPlayer.rare).nameIcon);
        imgIconDetail.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        imgIconDetail.SetNativeSize();
        imgBarProcess.fillAmount = Mathf.Min(_itemCollection.infoCollectionPlayer.amount / Mathf.Pow(2, _itemCollection.infoCollectionPlayer.level - 1), 1);
        txtProcess.text = _itemCollection.infoCollectionPlayer.amount + "/" + Mathf.Pow(2, _itemCollection.infoCollectionPlayer.level - 1);
        txtLevel.text = "Level " + _itemCollection.infoCollectionPlayer.level;
        Colection colection = LoadCollectionData.ins.GetColection(_itemCollection.infoCollectionPlayer.idBonus, _itemCollection.infoCollectionPlayer.rare);
        txtNameCollection.text = colection.nameItem;

        switch (_itemCollection.infoCollectionPlayer.idBonus)
        {
            case 0:
                strDetail = ConstName.totalDamage;
                break;
            case 1:
                strDetail = ConstName.totalHealth;
                break;
            case 2:
                strDetail = ConstName.totalFood;
                break;
            case 3:
                strDetail = ConstName.totalCoin;
                break;
        }
        if (_itemCollection.infoCollectionPlayer.amount >= Mathf.Pow(2, _itemCollection.infoCollectionPlayer.level - 1))
        {
            imgBarProcess.sprite = sprBarCanUpgrade;
            btnUpgrade.interactable = true;
            iconUp.SetActive(true);
            txtDetail.text = "+" + (colection.baseBonus + (colection.bonusPercentLevel * (_itemCollection.infoCollectionPlayer.level - 1))) + "<color=#00FF35> (+" + colection.bonusPercentLevel + "%)</color>" + strDetail;

        }
        else
        {
            imgBarProcess.sprite = sprBarNormal;
            btnUpgrade.interactable = false;
            iconUp.SetActive(false);
            txtDetail.text = "+" + (colection.baseBonus + (colection.bonusPercentLevel * (_itemCollection.infoCollectionPlayer.level - 1))) + strDetail;
        }
    }

    public void EventUpgrade()
    {
        LoadCollectionData.ins.EventUpgrade(itemCollectionSelected.infoCollectionPlayer, itemCollectionSelected.infoCollectionPlayer.idBonus);
        itemCollectionSelected.UpdateItem();
        ShowDetail(itemCollectionSelected);
        if (itemCollectionSelected.infoCollectionPlayer.amount >= Mathf.Pow(2, itemCollectionSelected.infoCollectionPlayer.level - 1))
        {
            btnUpgrade.interactable = true;
        }
        else
        {
            btnUpgrade.interactable = false;
        }
    }

    public void ClosePanel()
    {
        Destroy(gameObject);
    }

    public void CloseDetail()
    {
        panelDetail.SetActive(false);
    }

    public void CloseGacha()
    {
        panelGacha.SetActive(false);
        LoadCollectionPlayer();
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);
        UiManger.Ins.SetHeaderFooter(true);
    }

    public void UpdateBtnGacha()
    {
        if (Pref.GetRuby() < 100)
        {
            btnGachax1.interactable = btnGachax10.interactable = false;
        }
        else
        {
            btnGachax1.interactable = true;
            btnGachax10.interactable = Pref.GetRuby() >= 900;
        }
    }

    public void ShowTotalBonus(bool _active)
    {
        panelTotalBooster.SetActive(_active);
        if (!_active) return;
        txtTotalDamage.text = HelperFunc.ConvertValue(LoadCollectionData.ins.totalBonusDmg) + "%" + ConstName.totalDamage;
        txtTotalHealth.text = HelperFunc.ConvertValue(LoadCollectionData.ins.totalBonusHeath) + "%" + ConstName.totalHealth;
        txtTotalFood.text = HelperFunc.ConvertValue(LoadCollectionData.ins.totalBonusFood) + "%" + ConstName.totalFood;
        txtTotalCoin.text = HelperFunc.ConvertValue(LoadCollectionData.ins.totalBonusCoin) + "%" + ConstName.totalCoin;
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);

    }
}
