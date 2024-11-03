using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnenyMove : MonoBehaviour
{
  
    [HideInInspector]public string LayerAttack;
    private LayerMask Layer;

    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Transform targetFinish;
    [SerializeField] private EnenyController EnenyController;
    [SerializeField] private RangerAttack RangerAttack;

    private bool IsDie = false;
    private bool IsWin = false,IsPause = false;
    private Transform TargetMove;
    private bool isAttack = false;

    public string TagEneny;
    public float RangerScan = 10, Ranger=2, Speed=5;
    public LayerEneny LayerEneny;
    private TypeTroops troop;

    private void OnEnable()
    {
        // nav.isStopped = true;
        nav.speed = Speed;
        isAttack = false;
        SetLayer();
        RangerAttack.SetData(LayerAttack);
        EnenyController.SetTagEnenyAttack(LayerAttack);
    }
    private void SetLayer()
    {
        TagEneny = LayerEneny.GetName(typeof(LayerEneny), LayerEneny);
        if (TagEneny.Equals(ConstName.EnenyPlayer))
        {
            //Player

            LayerAttack = ConstName.EnenyBot;
        }
        else
        {
            //Bot
            LayerAttack = ConstName.EnenyPlayer;
        }

        Layer = (1 << LayerMask.NameToLayer(LayerAttack));
        gameObject.layer = LayerMask.NameToLayer(TagEneny);
        gameObject.tag = TagEneny;
    }
    private void FixedUpdate()
    {
      /*  if (nav.isStopped)
        {
            return;
        }*/
      if (!nav.enabled || IsDie || IsWin || IsPause)
        {
            return;
        }
        if (!isAttack)
        {
            TargetMove = NextPointPos(Layer);
            if (TargetMove == null)
            {
                TargetMove = targetFinish;
            }
           
            nav.SetDestination(TargetMove.position);
        }
    }
    public Transform NextPointPos(LayerMask layer)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, RangerScan, layer);
        float max = float.MaxValue;
        Transform target = null;
        if (colliders.Length > 0)
        {
            foreach (Collider cl in colliders)
            {
                float distance = Vector3.Distance(cl.transform.position, transform.position);
                if (distance < max)
                {
                    target = cl.gameObject.transform;
                    max = distance;
                }
            }
        }
        if (max < Ranger)
        {
            RangerAttack.SetIdEnenyAttack(target.GetComponent<EnenyController>().IdEneny);
            Attack(target, true);
        }
        return target;
    }
    public void Attack(Transform TargetAttack, bool status)
    {
        nav.isStopped = status;
       // nav.enabled = false;
        isAttack = status;
        EnenyController.RunAttack(TargetAttack,status);
    }
    public void PauseGame(bool Pause)
    {
        IsPause = Pause;
        
        if (Pause)
        {
            nav.isStopped = true;
        }
        else
        {
            if (!IsDie && !IsWin)
            {
                nav.isStopped = false;
            }
        }
    }
    public void ContinueMove(bool isWin)
    {
        IsWin = isWin;
        if (gameObject.activeSelf&& !IsDie && !IsWin)
        {
            //  nav.enabled = true;
            //nav.isStopped = false;
            if (!IsPause)
            {
                nav.Resume();
            }
            isAttack = false;
            RangerAttack.IsAttack = false;
        }
       /* nav.Resume();
        isAttack = false;
        RangerAttack.IsAttack = false;*/
    }
    public void SetDataTroop(float ranger,float Speed, TypeTroops TypeTroop)
    {
        IsDie = false;
        Ranger = ranger;
        this.Speed = Speed;
        troop = TypeTroop;
        RangerAttack.TypeTroop = (int)TypeTroop;
    }
    public void SetDieEneny()
    {
        IsDie = true;
    }
    public void SetFinish()
    {
        if (!IsWin)
        {
            IsWin = true;
            nav.isStopped = true;
            RangerAttack.IsAttack = false;
        }
    }
}
public enum LayerEneny
{
    EnenyPlayer = 1,
    EnenyBot = 2,
}

