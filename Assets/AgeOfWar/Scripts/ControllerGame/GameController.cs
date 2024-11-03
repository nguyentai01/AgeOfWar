using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Ins;
    [SerializeField] private List<EnenyController> ListCharactorPlays;
    [SerializeField] private List<EnenyController> ListCharactorBot;

    [SerializeField] private EnenyController CharactorPlays;
    [SerializeField] private EnenyController CharactorBot;
    [SerializeField] private Transform parrentPlayer;
    [SerializeField] private Transform parrentBot;

    [SerializeField] private Transform[] ListSpaws;
    [SerializeField] private HomeTrop Home;
    [SerializeField] private BattleUi BattleGame;
    [SerializeField] private GameObject Camera;

    public Listtrop[] Listtrop;
    public List<ListPlanet> ListPlanets;
    private int NumberWay = 0;
    private List<int> IdWay;
    private List<EnenyController> EnenyTroopCurrents = new List<EnenyController>();
    private bool IsWin = false, IsPause = false, GameIsRun = false;
    private IEnumerator Ie_CraeteBot;
    public float CoinGame = 0;
    private AsyncOperation asyncLoad;
    [SerializeField] private GameObject[] ListMaps;
    public Material[] listMatEnemys;
    public Material[] listMatPlayer;

    private int[] ArrayTroopLocks;

    private void Awake()
    {

        Ins = this;
        NumberWay = 0;
    }
    private void Start()
    {
        BattleGame.SetOff(true);
        Ie_CraeteBot = CreateCharactorBot();
        Listtrop = LoadResource.Ins.planetData.Plant[0].ListTroops;
        ListPlanets = LoadResource.Ins.PlanetInWayData.ListPlanets;
        GetCoin();
        SetMap(Pref.GetAgeBot());
        SetDataLockTroop();
        //StartCreateBot();
    }

    private void SetDataLockTroop()
    {
        ArrayTroopLocks = (int[])Pref.GetArrayDataUnlockTroops().Clone();
        for (int i=0;i< Listtrop.Length;i++)
        {
            for(int j =0;j< Listtrop[i].InforTroopPlayer.Length;j++)
            {
                if (Listtrop[i].InforTroopPlayer[j].Lock <= 0 /*&& ArrayTroopLocks[i*3+j]<=0*/)
                {
                    ArrayTroopLocks[i * 3 + j] = 1;
                }
            }
        }
        Pref.SetDataUnlockTroop(ArrayTroopLocks);
    }

    public void CreateCharactorPlayer(int Type = 0)
    {
        CreateCharactor(ListCharactorPlays, ListSpaws[0], (TypeTroops)Type, Listtrop[Pref.GetAgePlay()].InforTroopPlayer, Pref.GetAgePlay(), CharactorPlays,parrentPlayer);
    }
    private void StartCreateBot()
    {
        StartCoroutine(Ie_CraeteBot);
    }
    private void StopCreateBot()
    {
        StopCoroutine(Ie_CraeteBot);
    }
    private IEnumerator CreateCharactorBot()
    {
        while (!IsWin)
        {
            yield return new WaitForSeconds(ListPlanets[0].ListLevel[Pref.GetAgeBot()].ListWave[NumberWay].TimeDelay);
            if (IsWin)
            {
                yield break;
            }
            if (!IsPause)
            {
                IdWay = ListPlanets[0].ListLevel[Pref.GetAgeBot()].ListWave[NumberWay].IdWave;
                for (int i = 0; i < IdWay.Count; i++)
                {
                    if (IdWay[i] == 0)
                    {
                        continue;
                    }
                    for (int j = 0; j < IdWay[i]; j++)
                    {
                        CreateCharactor(ListCharactorBot, ListSpaws[1], (TypeTroops)(i + 1), Listtrop[Pref.GetAgeBot()].InforTroopBot, Pref.GetAgeBot(),CharactorBot,parrentBot);

                    }
                }

                if (NumberWay < ListPlanets[0].ListLevel[Pref.GetAgeBot()].ListWave.Count - 1)
                {
                    NumberWay++;
                }
                else
                {
                    NumberWay = Random.Range(0, ListPlanets[0].ListLevel[Pref.GetAgeBot()].ListWave.Count);
                }
            }

        }
    }
    private void CreateCharactor( List<EnenyController> ListEneny, Transform Pos, TypeTroops typeTroop, InforTrop[] Troop, int age,EnenyController enenyBase,Transform parrent)
    {
/*        for (int i = 0; i < ListEneny.Count; i++)
        {
            if (!ListEneny[i].gameObject.activeSelf)
            {
                ListEneny[i].ResetEney(i, typeTroop, Troop[(int)typeTroop - 1], Pos, age);
                ListEneny[i].gameObject.SetActive(true);
                EnenyTroopCurrents.Add(ListEneny[i]);
                return;
            }
        }*/

        foreach (EnenyController eneny in GetEneny(ListEneny))
        {
            eneny.ResetEney(ListEneny.IndexOf(eneny), typeTroop, Troop[(int)typeTroop - 1], Pos, age);
            eneny.gameObject.SetActive(true);
            EnenyTroopCurrents.Add(eneny);
            return;
        }
        if (ListEneny.Count > 25)
        {
            return;
        }

        EnenyController ene = Instantiate(enenyBase, parrent);
        ListEneny.Add(ene);
        ene.ResetEney(ListEneny.Count, typeTroop, Troop[(int)typeTroop - 1], Pos, age);
        ene.gameObject.SetActive(true);
        EnenyTroopCurrents.Add(ene);
    }

    private IEnumerable<EnenyController> GetEneny(List<EnenyController> ListEneny)
    {
        foreach (EnenyController eneny in ListEneny)
        {
            if (!eneny.gameObject.activeSelf)
            {
                yield return eneny;
            }
        }
    }
    public void RemoveListTroop(EnenyController Control)
    {
        EnenyTroopCurrents.Remove(Control);
    }
    public void SetFinishGame()
    {
        IsWin = true;
        foreach (EnenyController En in EnenyTroopCurrents)
        {
            En.SetFinishGame();
        }
    }
    public void SetPauseGame(bool Pause)
    {
        IsPause = Pause;
        if (GameIsRun)
        {
            foreach (EnenyController En in EnenyTroopCurrents)
            {
                En.SetPause(IsPause);
            }
            BattleGame.SetResume(true);
        }
        else
        {
            GameIsRun = true;
            BattleGame.SetOff(false);
            StartCreateBot();
            Pref.ResetCoinInGame();
            if (Pref.GetStateTut("Tut01") == 0)
            {
                UiManger.Ins.tutorialController.ShowTut("Tut01");
                Time.timeScale = 0;
            }
            BattleGame.SetResume(false);

            Firebase.Analytics.FirebaseAnalytics.LogEvent("START_LEVEL_" + (Pref.GetAgeBot() + 1).ToString("000"));
        }

    }
    public void SetCoin(float coin)
    {
        Pref.SetCoin(coin);
        CoinGame = Pref.GetCoin();

        UiManger.Ins.SetTextHeader(0, CoinGame);
    }
    public bool CheckCoinUpdate(float coin, bool IsUpgradeFood = true)
    {
        if (coin <= CoinGame)
        {
           
            SetCoin(-coin);
            EventUi.Ins.CheckButtonByCoin();
            if (IsUpgradeFood)
            {
                UpdateFood();
            }
            else
            {
                UpdateHp();
            }
            return true;
        }
        return false;
    }
    public void UpgradeAge(float coin)
    {
        if (coin <= CoinGame)
        {
            SetCoin(-coin);
            Pref.SetAgePlay();
            SetTextUpgradeAge();
            Home.SetCurrentBarracks();
            EventUi.Ins.GetDataTroops();
        }
    }
    private void SetTextUpgradeAge()
    {
        Home.UpdateHp(UiManger.Ins.HpUpdate());
        UiManger.Ins.SetUpdateAge();
    }
    private void UpdateFood()
    {
        Pref.SetLevelUpdateFood();
        UiManger.Ins.SetUpdateFood();
    }
    private void UpdateHp()
    {
        Home.UpdateHp(UiManger.Ins.HpUpdate());
        Pref.SetLevelHpHome();
        UiManger.Ins.SetUpdateHp();
    }
    private void GetCoin()
    {
        CoinGame = Pref.GetCoin();
        UiManger.Ins.SetTextHeader(0, CoinGame);

    }
    public void StartLoadScene(float time)
    {
        StartCoroutine(LoadScene(time));
    }
    private IEnumerator LoadScene(float timeLoad)
    {
        UiManger.Ins.SetOnObject(0);
        UiManger.Ins.SetUiPlayGames(0);
        asyncLoad = SceneManager.LoadSceneAsync(ConstName.Playing);
        asyncLoad.allowSceneActivation = false;
        yield return new WaitForSeconds(timeLoad);
        asyncLoad.allowSceneActivation = true;
        UiManger.Ins.unlockMode.CheckUnLockMode();

    }
    public void SetMap(int index)
    {
        for (int i = 0; i < ListMaps.Length; i++)
        {
            ListMaps[i].SetActive(index == i);
        }
    }

    public float GetHpHomePlayer()
    {
       return  Home.GetHpHome();
    }

    public void SnakeCam()
    {
        Camera.transform.DOShakePosition(1.3f,6,6,10).SetEase(Ease.OutQuad);
        if (Pref.GetVib() == 1)
        {
            Handheld.Vibrate();

        }
    }
}
