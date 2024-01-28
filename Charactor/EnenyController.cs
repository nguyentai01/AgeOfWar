using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnenyController : MonoBehaviour
{
    [SerializeField] private EnenyMove EnenyMove;
    public event Action EventDie;
    private bool Attack = false;
    private Transform TargetAttack;
    private int HP = 50,Dmg =10;
    private float SpeedAttack = 1;
    private EnenyController EnenyAttack;
    public int IdEneny = 0;
    private int HPMax = 50;
    private string TagAttack;
    [SerializeField] private Image HPui;
    [SerializeField] private Transform PointStartShot;
    private bool IsDie = false;
    private TypeTroops TypeTroop = TypeTroops.Inf;
   
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
        EnenyMove.SetDieEneny();
        gameObject.tag = ConstName.None;
        gameObject.layer =LayerMask.NameToLayer(ConstName.None) ;

    }

    private void FinishAttack()
    {
        Attack = false;
        TargetAttack = null;
        EnenyAttack = null;
        StopAllCoroutines();
        EnenyMove.ContinueMove();
    }
    public void RunAttack(Transform TargetAttack, bool status)
    {
        Attack = status;
        this.TargetAttack = TargetAttack;
        EnenyAttack = TargetAttack.GetComponent<EnenyController>();
        EnenyAttack.AddEnenyAttack(FinishAttack, status);
        switch ((int)TypeTroop)
        {
            case 1:
            case 3:
                if (Attack)
                {
                    StartCoroutine(StartAttack());
                }
                else
                {
                    StopAllCoroutines();
                    // EnenyMove.ContinueMove();
                    EnenyAttack = null;
                }
                break;
            case 2:
                if (Attack)
                {
                    StartCoroutine(AttackRanger());
                }
                break;
        }
    }
    public void GetDmg(int Dmg)
    {
        HP -= Dmg;
        DOTween.To(() => HPui.fillAmount, x => HPui.fillAmount = x, (float)HP/(float)HPMax, 0.1f);
        if (HP <= 0)
        {
            DieEneny();
            if (EnenyAttack != null)
            {
                EventDie = null;
                EnenyAttack.AddEnenyAttack(FinishAttack, false);
                EnenyAttack = null;
            }
            Invoke("SetOffObject", 1);
        }
        
    }
    private void SetOffObject()
    {
        gameObject.SetActive(false);

    }
    private IEnumerator StartAttack()
    {
        RotaEneny();
        while (Attack && !IsDie)
        {
            yield return new WaitForSeconds(SpeedAttack);
            //Attack
            if (IsDie)
            {
                yield break;
            }
            switch ((int)TypeTroop)
            {
                case 1:// Inf
                    EnenyAttack.GetDmg(Dmg);
                    break;

                case 3:// Cav
                    EnenyAttack.GetDmg(Dmg);
                    break;
            }
        }
    }
    private IEnumerator AttackRanger()
    {
        RotaEneny();
        yield return new WaitForSeconds(SpeedAttack);
        if (EnenyAttack != null)
        {
            ManagerArrow.Ins.SetArrow(Dmg, EnenyAttack.transform, TagAttack, PointStartShot);
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
    public void ResetEney(int id,TypeTroops TypeTroop,InforTrop Infor,Transform Pos)
    {
        transform.position = Pos.position;
        this.TypeTroop = TypeTroop;
        HPui.fillAmount = 1;
        HPMax = Infor.Hp;
        HP = HPMax;
        Dmg = Infor.Dmg;
        SpeedAttack = Infor.SpeedAttack;
        EnenyMove.SetDataTroop(Infor.Ranger, Infor.Speed, TypeTroop);
        IdEneny = id;
        
    }
    public void SetTagEnenyAttack(string tag)
    {
        TagAttack = tag;
    }
}
public enum TypeTroops
{
    Inf = 1,
    Rang = 2,
    Cav = 3
}
