using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWeapon : MonoBehaviour
{
    private Transform target;
    private int Dmg = 10;
    private string TagAttack;
    private Tweener TwMove;
    private float speed = 10;

    private void OnEnable()
    {
        //c
        float dis = Vector3.Distance(transform.position, target.position);
        TwMove = transform.DOMove(target.position, dis/speed).OnComplete(()=>{ gameObject.SetActive(false); });
    }
    public void SetDataArrow(int dmg,Transform target,string tagAttack, Transform pos)
    {
        this.target = target;
        Dmg = dmg;
        TagAttack = tagAttack;
        transform.position = pos.position;
        transform.rotation = Quaternion.Euler(pos.eulerAngles+new Vector3(0,-90,0));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagAttack))
        {
            TwMove.Pause();
            other.gameObject.GetComponent<EnenyController>().GetDmg(Dmg);
            gameObject.SetActive(false);
        }
    }
}//Tsetts
