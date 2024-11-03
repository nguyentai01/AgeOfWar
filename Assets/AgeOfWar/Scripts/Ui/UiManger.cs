using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class UiManger : MonoBehaviour
{
    public static UiManger Ins { get; private set; }
    public Image FoodImg;
    [SerializeField] private TextMeshProUGUI FoodTxt;
    [SerializeField] private TextMeshProUGUI[] ListFoodTroops;

    [SerializeField] private ProccesFood FoodManager;
    [SerializeField] private GameObject[] UiHomes;
    [SerializeField] private GameObject[] UiSelects;
    [SerializeField] private GameObject[] UiPlayGames;
    [SerializeField] private GameObject[] ListObjects;

    [SerializeField] private TextMeshProUGUI[] UpgradeFoodTxts;
    [SerializeField] private TextMeshProUGUI[] UpgradeHpTxts;
    [SerializeField] private TextMeshProUGUI[] UpgradeAgeTxts;

    [SerializeField] private TextMeshProUGUI[] textHeaders;
    [SerializeField] private Color ColorBtnSelect;
    [SerializeField] private UpgradeUi UpgradeUi;

    [SerializeField] private TextMeshProUGUI coinEnding;

    [SerializeField] private Button[] buttonUpgrades;
    [SerializeField] private EndingUi endingUi;
    [SerializeField] private Image bgHeader;
    private List<TroopHome> ListLevelHomes;

    private DataGame DataGame;

    public UnlockMode unlockMode;
    public TutorialController tutorialController;
    [SerializeField] GameObject failPuchase;
    [SerializeField] GameObject checkInternetVideoReward;
    public GameObject btnRemoveAds;
    [SerializeField] GameObject prfBuyRemoveAds;
    private void Awake()
    {
        Ins = this;
    }
    private void Start()
    {
        DataGame = LoadResource.Ins.DataGame;
        ListLevelHomes = LoadResource.Ins.GetListLevelHomes(Pref.GetplantPlay());
        UpdateTxtFood();

        UpdateTxtAge();
        SetCoinCurrent();
        Btn_SelectHomeUi(2);
        GetRuby();

        btnRemoveAds.SetActive(Pref.GetRemoveAds() == 0);
        ShowRating();
    }

    public bool GetFood(int Food)
    {
        return FoodManager.GetFood(Food);
    }
    public void SetFoodTxt(float FoodCurrent)
    {
        FoodTxt.text = HelperFunc.ConvertValue(FoodCurrent) + "";
    }
    public void UpdateFoodTroop(List<int> ListFoods)
    {
        for (int i = 0; i < ListFoods.Count; i++)
        {
            ListFoodTroops[i].text = "" + HelperFunc.ConvertValue(ListFoods[i]);
        }
    }
    public void Btn_SelectHomeUi(int id)
    {
        SetActiveUi(id);
        if (id == 1)
        {
            UpgradeUi.Btn_SelectUpgrade(0);
        }
        else
        {
            SetTitleHeader(id);

        }

    }
    private void SetActiveUi(int index)
    {
        for (int i = 0; i < UiHomes.Length; i++)
        {
            UiHomes[i].SetActive(index == i);
            if (UiSelects[i])
            {
                UiSelects[i].SetActive(index == i);
            }
        }

    }
    public void SetUpdateFood()
    {
        UpdateTxtFood();
        SetCoinCurrent();

    }
    private void UpdateTxtFood()
    {
        UpgradeFoodTxts[0].text = string.Format(" {0:0.00} /s", GetFoodCurrent());
        UpgradeFoodTxts[1].text = string.Format(" {0:0}", HelperFunc.ConvertValue(GetCoinUpdate()));
    }
    private float GetFoodCurrent()
    {
        float FoodGet = ((Pref.GetLevelUpdateFood() * DataGame.FoodPerStep) + DataGame.FoodDefault);
        FoodGet += ((FoodGet * LoadCollectionData.ins.totalBonusFood) / 100f);

        return FoodGet;
    }
    public void SetUpdateHp()
    {
        UpdateTxtHp();
        SetCoinCurrent();

    }
    public void UpdateTxtHp()
    {
        UpgradeHpTxts[0].text = string.Format(" {0:0}", HelperFunc.ConvertValue(GameController.Ins.GetHpHomePlayer()));
        UpgradeHpTxts[2].text = string.Format("(+ {0:0})", HelperFunc.ConvertValue(HpUpdate()));
        UpgradeHpTxts[1].text = string.Format(" {0:0}", HelperFunc.ConvertValue(GetCoinUpdateHp()));

    }

    private void SetCoinCurrent()
    {
        float CoinCurrent = Pref.GetCoin();
        UpgradeUi.SetEnableButton(0, GetCoinUpdate() < CoinCurrent);
        UpgradeUi.SetEnableButton(1, GetCoinUpdateHp() < CoinCurrent);
        UpgradeUi.SetEnableButton(2, GetCoinUpgradeAge() < CoinCurrent);
    }
    public void SetUpdateAge()
    {
        UpdateTxtAge();
        SetUpdateHp();
    }
    private void UpdateTxtAge()
    {
        UpgradeAgeTxts[0].text = "" + HelperFunc.ConvertValue(GetCoinUpgradeAge());
    }
    public float GetCoinUpdate()
    {
        return (DataGame.DefaultCoinFoodUpgrade * Mathf.Pow(DataGame.RatioUpgradeFood, Pref.GetLevelUpdateFood()));
    }
    public float GetCoinUpdateHp()
    {
        return (DataGame.DefaultCoinHpUpgrade * Mathf.Pow(DataGame.RatioUpgradeHp, Pref.GetLevelHpHome()));
    }
    public int GetCoinUpgradeAge()
    {
        return LoadResource.Ins.PlantUpdateHome.PlantHomes[Pref.GetplantPlay()].ListLevelHomes[Pref.GetAgePlay()].CoinUpdate;
    }

    public void CheckButtonUpgrade()
    {
        float currentCoin = Pref.GetCoin();
        buttonUpgrades[0].interactable = currentCoin >= GetCoinUpdate();
        buttonUpgrades[1].interactable = currentCoin >= GetCoinUpdateHp();
        buttonUpgrades[2].interactable = currentCoin >= GetCoinUpgradeAge();

    }
    public float HpUpdate()
    {
        return 1 + (ListLevelHomes[Pref.GetAgePlay()].HpHomePlay * Mathf.Pow(DataGame.HpPerStep, Pref.GetLevelHpHome()) * (DataGame.HpPerStep - 1));
    }
    public void SetTitleHeader(int id)
    {
        if (id >= ConstName.TitleHeaders.Length)
        {
            return;
        }
        if (id == 2)
        {
            textHeaders[1].text = ConstName.TitleHeaders[2] + " " + (Pref.GetAgeBot() + 1);
        }
        else
        {

            textHeaders[1].text = ConstName.TitleHeaders[id];
        }

    }
    public void GetRuby()
    {
        textHeaders[2].text = "" + HelperFunc.ConvertValue(Pref.GetRuby());

    }
    public void SetTextHeader(int id, float index)
    {
        textHeaders[id].text = HelperFunc.ConvertValue(index) + "";
    }
    public void SetUiPlayGames(int index)
    {
        if (index >= UiPlayGames.Length)
        {
            return;
        }
        for (int i = 0; i < UiPlayGames.Length; i++)
        {
            UiPlayGames[i].SetActive(index == i);

        }
    }

    public void SetEnding(bool status)
    {
        endingUi.SetFinishUi(status);
        SetOnObject(1);

    }

    public void SetOnObject(int index)
    {
        for (int i = 0; i < ListObjects.Length; i++)
        {
            if (i == 4 || i == 5)
            {
                continue;
            }
            ListObjects[i].SetActive(i == index);

        }
        if (index == 1)
        {
            int coinGame = Pref.GetCoinInGame();
            //coinGame = coinGame>10 ?coinGame: 10;
            if (coinGame <20)
            {
                coinEnding.text = HelperFunc.ConvertValue(20) + "";
            }
            else
            {
                coinEnding.text = HelperFunc.ConvertValue(coinGame) + "";
            }
            FoodManager.PauseGame(true);
        }
    }

    public void SetHeaderFooter(bool status)
    {
        ListObjects[4].SetActive(status);
        ListObjects[5].SetActive(status);

    }

    public void EventFailPay()
    {
        Instantiate(failPuchase, transform);
    }

    public void CheckInternet(bool _active)
    {
        checkInternetVideoReward.SetActive(_active);
    }

    public void OpenBuyRemoveAds()
    {
        Instantiate(prfBuyRemoveAds, transform);
    }

    private void ShowRating()
    {
        if (Pref.CheckWinLv() == 1 && Pref.GetRating()<1)
        {
            SetOnObject(3);
        }
    }

    public void SetImgHeader(bool status)
    {
        bgHeader.enabled = !status;
    }
}
