using AppsFlyerSDK;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class EnenyController : MonoBehaviour
{
    private bool Attack = false, IsPause = false;
    private Transform TargetAttack;
    private float HPMax = 50, HP = 50, SpeedAttack = 1, Dmg = 10;
    private EnenyController EnenyAttack;
    private int Coin;
    private string TagAttack;
    private bool IsDie = false;
    private TypeTroops TypeTroop = TypeTroops.Inf;
    private AniController ControlEneny;

    public int IdEneny = 0;
    public event Action EventDie;
    public TypeTarget TypeTarget = TypeTarget.Troop;

    [SerializeField] private EnenyMove EnenyMove;
    [SerializeField] private RectTransform HPui;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Transform PointStartShot;
    [SerializeField] private GameObject HpHome;
    [SerializeField] private HomeTrop home;
    float sizeXHpUI;

    private void Awake()
    {
        sizeXHpUI = HPui.sizeDelta.x;
    }

    public void AddEnenyAttack(Action Action, bool isAdd = true)
    {
        if (isAdd)
        {
            EventDie += Action;
        }
        else
        {
            EventDie -= Action;
           
        }
        
    }

    private void DieEneny()
    {
        EventDie?.Invoke();
        Attack = false;
        TargetAttack = null;
        EnenyAttack = null;
        StopAllCoroutines();
        if (!gameObject.tag.Equals(ConstName.EnenyPlayer))
        {
            //CreateCoin
            CoinManager.Ins.SetCoin(Coin, transform.position);
        }

        if ((int)TypeTarget == 2)
        {
            //Home

            GameController.Ins.SetFinishGame();
            SetNextLv();
        }
        else//Trop
        {

            GameController.Ins.RemoveListTroop(this);
            try
            {
                ControlEneny.StartAnim(AnimationName.Ani_Die, 1);

            }
            catch { }
            EnenyMove.SetDieEneny();
        }
        gameObject.tag = ConstName.None;
        gameObject.layer = LayerMask.NameToLayer(ConstName.None);
    }
    private void SetNextLv()
    {
        // ManagerAds.ins.ShowInterstitial();
        
        if (gameObject.tag.Equals(ConstName.EnenyPlayer))
        {
            //Losed
            home?.Losed();
            StartCoroutine(Ie_SetOnUiFinishGame(false));

            Firebase.Analytics.FirebaseAnalytics.LogEvent("LOSED_LEVEL" + (Pref.GetAgeBot() + 1).ToString("000"));
        }
        else
        {
            //WIn
            if (Pref.GetAgeBot()==Pref.GetCountLvUnlock())
            {
                Pref.SetCountLvUnlock();
            }
            Pref.SetAgeBot(1);
            Pref.SetWinLv(1);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("WIN_LEVEL_" + Pref.GetAgeBot().ToString("000"));
            int level = Pref.GetAgeBot();
            if (level < 21)
            {
               
                if (Pref.GetLevelCompleted(level) == 0)
                {
                    AppsFlyer.sendEvent("completed_level_" + level, null);
                    Pref.SetLevelCompleted(level);
                }
                else if (level == 5)
                {
                    AppsFlyer.sendEvent("completed_level_" + 6, null);
                }
            }
            StartCoroutine(Ie_SetOnUiFinishGame(true));
        }
        GameController.Ins.SetPauseGame(true);
       
        Pref.SetNumberOfPlay();
       
    }

    private IEnumerator Ie_SetOnUiFinishGame(bool status)
    {
        yield return new WaitForSeconds(1);
        UiManger.Ins.SetEnding(status);
    }
    private void FinishAttack()
    {

        Attack = false;
        TargetAttack = null;
        EnenyAttack = null;
        StopAllCoroutines();
        if ((int)TypeTarget == 1)
        {
            ControlEneny.StartAnim(AnimationName.Ani_Move, 1);
            EnenyMove.ContinueMove(false);
        }
        else
        {
            // EnenyMove.ContinueMove(true);
        }
    }
    public void SetFinishGame()
    {
        Attack = false;
        StopAllCoroutines();
        EnenyMove.SetFinish();
    }

    public void StartDmgEneny()
    {
        if (EnenyAttack == null)
        {
            return;
        }
        if ((int)TypeTroop != 2)
        {

            EnenyAttack.GetDmg(Dmg);


        }
        else
        {
            ManagerArrow.Ins.SetArrow(Dmg, EnenyAttack.transform, TagAttack, PointStartShot);
        }
    }

    public void GetDmg(float Dmg)
    {
        HP -= Dmg;
        SetHpText();

        //HPui.DOSizeDelta(new Vector2(sizeXHpUI * ((float)HP / (float)HPMax), HPui.sizeDelta.y), .1f);
        if (HP <= 0)
        {
            DieEneny();
            if (EnenyAttack != null)
            {
                EventDie = null;
                EnenyAttack.AddEnenyAttack(FinishAttack, false);
                EnenyAttack = null;
            }
            Invoke("SetOffObject", 2);
        }
        DisPlayer(true);

    }
    private void SetHpText()
    {
        if (hpText)
        {
            float hpCurrent = Mathf.Clamp(HP, 0, float.MaxValue);
            hpText.text = "" + HelperFunc.ConvertValue(hpCurrent);
            HPui.sizeDelta = new Vector2(sizeXHpUI*((float)HP / (float)HPMax), HPui.sizeDelta.y);
        }
    }
    private void SetOffObject()
    {
        if ((int)TypeTarget == 1)
        {

            Destroy(ControlEneny.gameObject);
            gameObject.SetActive(false);
        }

    }
    private IEnumerator StartAttack()
    {
        RotaEneny();
        while (Attack && !IsDie)
        {
            ControlEneny.StartAnim(AnimationName.Ani_Attack, 1);
            AudioManager.Ins.SetAudioAttack(TypeTroop, (int)EnenyMove.LayerEneny == 1 ? Pref.GetAgePlay() : Pref.GetAgeBot());
            yield return new WaitForSeconds(SpeedAttack);
            //Attack
            if (IsDie)
            {
                yield break;
            }
            switch ((int)TypeTroop)
            {
                case 1:// Inf
                       //  EnenyAttack.GetDmg(Dmg);
                    break;

                case 3:// Cav
                       //   EnenyAttack.GetDmg(Dmg);
                    break;
            }
            yield return new WaitForSeconds(.5f);
            ControlEneny.StartAnim(AnimationName.Ani_Die, 1);
        }
    }

    private IEnumerator AttackRanger()
    {
        RotaEneny();
        yield return new WaitForSeconds(.5f);
        ControlEneny.StartAnim(AnimationName.Ani_Attack, 1);
        AudioManager.Ins.SetAudioAttack(TypeTroop, (int)EnenyMove.LayerEneny == 1 ? Pref.GetAgePlay() : Pref.GetAgeBot());
        yield return new WaitForSeconds(SpeedAttack - .5f);

        if (EnenyAttack != null)
        {
            // ManagerArrow.Ins.SetArrow(Dmg, EnenyAttack.transform, TagAttack, PointStartShot);
            EnenyAttack.AddEnenyAttack(FinishAttack, false);
        }

        FinishAttack();
        //
    }
    private void RotaEneny()
    {
        Vector3 dir = transform.position - TargetAttack.position;
        float radi = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
        transform.DORotate(new Vector3(0, 180 + radi, 0), .3f);
    }
    public void ResetEney(int id, TypeTroops TypeTroop, InforTrop Infor, Transform Pos, int age)
    {
        transform.position = Pos.position;
        this.TypeTroop = TypeTroop;
        TypeTarget = TypeTarget.Troop;
        HPui.sizeDelta=new Vector2(sizeXHpUI,HPui.sizeDelta.y);
        HPMax = Infor.Hp;
        Dmg = Infor.Dmg;
        SpeedAttack = Infor.SpeedAttack;
        EnenyMove.SetDataTroop(Infor.Ranger, Infor.Speed, TypeTroop);
        IdEneny = id;
        Coin = Infor.Coin;
        ControlEneny = Instantiate(EnenyManager.Ins.EnenyInAges[age].ListEnenys[(int)TypeTroop - 1], transform);
        ControlEneny.transform.localPosition = Vector3.zero;
        ControlEneny.transform.localRotation =Quaternion.Euler(0,0,0);


        if ((int)EnenyMove.LayerEneny == 1)
        {
            //Player
            Dmg += ((Dmg * LoadCollectionData.ins.totalBonusDmg) / 100f);
            HPMax += ((HPMax * LoadCollectionData.ins.totalBonusHeath) / 100f);
            Coin += (int)((Coin * LoadCollectionData.ins.totalBonusCoin) / 100f);
            ControlEneny.SetMaterial(GameController.Ins.listMatPlayer[(Pref.GetAgePlay()*3)+(int)TypeTroop-1]);

        }
        else
        {
            //Bot
            ControlEneny.SetMaterial(GameController.Ins.listMatEnemys[(Pref.GetAgeBot()*3) + (int)TypeTroop - 1]);
        }
        ControlEneny.gameObject.SetActive(true);

        HP = HPMax;
        SetHpText();

    }
    public void ResetEney(int id, float Hp)
    {
        TypeTarget = TypeTarget.Home;
        HPui.sizeDelta = new Vector2(sizeXHpUI, HPui.sizeDelta.y);
        HPMax = Hp;
        HP = HPMax;
        IdEneny = id;
        SetHpText();
    }
    public void SetTagEnenyAttack(string tag)
    {
        TagAttack = tag;
    }
    public void UpdateHp(float hp)
    {
        HP += hp;
        HPMax += hp;
        SetHpText();
    }

    private void DisPlayer(bool status)
    {
        if (IdEneny == 99)
        {
            HpHome.SetActive(status);
        }
    }
}
public enum TypeTroops
{
    Inf = 1,
    Rang = 2,
    Cav = 3
}
public enum TypeTarget
{
    Troop = 1,
    Home = 2,
}