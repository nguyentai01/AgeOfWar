using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EventUi : MonoBehaviour
{
    public static EventUi Ins;

    public Listtrop[] InforTroopPlayer;
    private List<int> ListFoodTroop = new List<int>();
    [SerializeField] private ProccesFood FoodManager;
    [SerializeField] private CollectionMenu MenuUpgradeCard;
    [SerializeField] private UpgradeUi UpgradeUi;
    [SerializeField] private UnLockTroops unlockTropUi;
    [SerializeField] private ProccesFood processFood;
    [SerializeField] private UiPlaying uiPlaying;
    [SerializeField] private GameObject uiGame;
    [SerializeField] private CanvasGroup gr;
    private void Awake()
    {
        Ins = this;
    }
    private void Start()
    {
        GetDataTroops();
        AudioManager.Ins.SetMusic(AudioManager.Ins.bg_mussic);
        CheckSpawFood();
    }
    public void Btn_SpamEneny(int TypeTroop = 1)
    {
        if (UiManger.Ins.GetFood(InforTroopPlayer[Pref.GetAgePlay()].InforTroopPlayer[TypeTroop - 1].Food))
        {
            GameController.Ins.CreateCharactorPlayer(TypeTroop);
        }
        CheckSpawFood();
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }



    public void GetDataTroops()
    {
        if (ListFoodTroop.Count > 0)
        {
            ListFoodTroop.Clear();
        }
        InforTroopPlayer = LoadResource.Ins.planetData.Plant[Pref.GetplantPlay()].ListTroops;
        for (int i = 0; i < 3; i++)
        {
            ListFoodTroop.Add(InforTroopPlayer[Pref.GetAgePlay()].InforTroopPlayer[i].Food);
        }
        UiManger.Ins.UpdateFoodTroop(ListFoodTroop);
    }
    public void Btn_GetFood(int Food)
    {
        FoodManager.GetFoodClick(Food);
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);
        CheckSpawFood();


    }

    public void CheckSpawFood()
    {
        uiPlaying.CheckFoodUi(ListFoodTroop, FoodManager.GetFood());
    }
    public void Btn_SelectUiHome(int id)
    {
        UiManger.Ins.Btn_SelectHomeUi(id);
        if (id != 3)
        {
            MenuUpgradeCard.DestroyPlane();
        }
        if (id != 4)
        {
            MenuUpgradeCard.DestroyShop();

        }
       
        UiManger.Ins.SetImgHeader(id !=2);
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }
    public void Btn_PauseGame(bool IsPause)
    {
        GameController.Ins.SetPauseGame(IsPause);
        
        FoodManager.PauseGame(IsPause);
        if (!IsPause)
        {
            UiManger.Ins.SetUiPlayGames(1);
            AudioManager.Ins.SetMusic(AudioManager.Ins.bg_battle);

        }
        else
        {
            InitAds.ins.ShowInterstitial();
            UiManger.Ins.SetUiPlayGames(0);
            AudioManager.Ins.SetMusic(AudioManager.Ins.bg_mussic);

        }
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }
    public void Btn_GetFoodByAds()
    {
        //  Btn_GetFood(LoadResource.Ins.planetData.Plant[Pref.GetplantPlay()].ListTroops[Pref.GetAgePlay()].FoodReward);
        InitAds.ins.ShowRewarded((x) =>
        {
            if (x)
            {
                Btn_GetFood(LoadResource.Ins.planetData.Plant[Pref.GetplantPlay()].ListTroops[Pref.GetAgePlay()].FoodReward);

            }
        });
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }
    public void Btn_UpgradeFood()
    {
        GameController.Ins.CheckCoinUpdate(UiManger.Ins.GetCoinUpdate());
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);
        processFood.SetFoodGetByLv();
    }
    public void Btn_UpgradeHpHome()
    {
        //  Pref.SetLevelHpHome();
        GameController.Ins.CheckCoinUpdate(UiManger.Ins.GetCoinUpdateHp(), false);
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }
    public void Btn_UpgradeAge()
    {
        if (Pref.GetAgePlay() < 5)
        {
            GameController.Ins.UpgradeAge(UiManger.Ins.GetCoinUpgradeAge());

            if (Pref.GetAgePlay() == 5)
            {
                UpgradeUi.SetOffButtonUpdate(2, false);

            }
            else
            {
                UpgradeUi.SetContentAgeUi();
            }
        }
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);
    }
    public void EventSpeedUp()
    {
        InitAds.ins.ShowRewarded(((bool _complete) =>
        {
            if (_complete)
            {
                Time.timeScale = 1.5f;
            }

        }));
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }

    public void CheckButtonByCoin()
    {
        unlockTropUi.CheckButtonByCoin();
        UiManger.Ins.CheckButtonUpgrade();
    }

    public void Btn_X2Coin()
    {
        InitAds.ins.ShowRewarded((x) =>
        {
            if (x)
            {
                int coinGame = Pref.GetCoinInGame();
                if (coinGame >= 20)
                {
                    GameController.Ins.SetCoin(coinGame);

                }
                else
                {
                    GameController.Ins.SetCoin(40);

                }
                GameController.Ins.StartLoadScene(2);
            }
        });
    }

    public void Btn_Closed()
    {
        InitAds.ins.ShowInterstitial();
        int coinGame = Pref.GetCoinInGame();
        if (coinGame <20)
        {
            GameController.Ins.SetCoin(20);
        }
        GameController.Ins.StartLoadScene(2);
    }

    public void CheatCoinRuby(bool _isCoin)
    {
        if (_isCoin)
        {
            GameController.Ins.SetCoin(99999999);
        }
        else
        {
            Pref.SetRuby(99999999);
            UiManger.Ins.GetRuby();
        }
    }

    public void Btn_Setting()
    {
        UiManger.Ins.SetOnObject(2);
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }


    public void EventCompleteBuyRemoveAds()
    {
        Pref.SetRemoveAds();
        UiManger.Ins.btnRemoveAds.SetActive(false);
        InitAds.ins.HideBanner();
    }

    public void Btn_ActiveUi()
    {
        //uiGame.SetActive(!uiGame.activeSelf);
        gr.alpha = gr.alpha ==1? 0:1;
        uiPlaying.SetOffMoldePlaying(gr.alpha != 0);
    }
}
