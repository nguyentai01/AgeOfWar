using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnenyMove : MonoBehaviour
{
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private Transform targetFinish;
    public LayerEneny LayerEneny;
    [HideInInspector]public string LayerAttack;
    private LayerMask Layer;
    public string TagEneny;
    [SerializeField] private EnenyController EnenyController;
    [SerializeField] private RangerAttack RangerAttack;
    private Transform TargetMove;
    public float RangerScan = 10;
    public float Ranger = 2;
    public float Speed = 5;
    private bool isAttack = false;
    [SerializeField] private MeshRenderer Mesh;
    [SerializeField] private Material[] ListMaterial;
    private int DisAttack =2;
    private bool IsDie = false;
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
            Mesh.material = ListMaterial[0];
            LayerAttack = ConstName.EnenyBot;
        }
        else
        {
            //Bot
            Mesh.material = ListMaterial[1];

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
      if (!nav.enabled || IsDie)
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
    public void ContinueMove()
    {
        if (gameObject.activeSelf&& !IsDie)
        {
            nav.Resume();
            //  nav.enabled = true;
            //nav.isStopped = false;
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
        RangerAttack.TypeTroop = (int)TypeTroop;
    }
    public void SetDieEneny()
    {
        IsDie = true;
    }
}
public enum LayerEneny
{
    EnenyPlayer = 1,
    EnenyBot = 2,
}

