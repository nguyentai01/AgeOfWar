using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowWeapon : MonoBehaviour
{
    private Transform target;
    private float Dmg = 10;
    private string TagAttack;
    private Tweener TwMove;
    private float speed = 10;
    [SerializeField] private GameObject[] weapon;
    private void OnEnable()
    {
        //c
        if (target == null)
        {
            return;
        }
        SetWeapon();

        float dis = Vector3.Distance(transform.position, target.position);
        TwMove = transform.DOMove(target.position + new Vector3(0, .5f, 0), dis / speed).OnComplete(() => { gameObject.SetActive(false); });
    }

    private void SetWeapon()
    {
        int age;
        if (TagAttack.Equals(ConstName.EnenyPlayer))
        {
            age = Pref.GetAgeBot();
        }
        else
        {
            age = Pref.GetAgePlay();

        }
        for (int i = 0; i < weapon.Length; i++)
        {
            weapon[i].SetActive(i == age);
        }
    }
    public void SetDataArrow(float dmg, Transform target, string tagAttack, Transform pos)
    {
        this.target = target;
        Dmg = dmg;
        TagAttack = tagAttack;
        transform.position = pos.position;
        transform.rotation = Quaternion.Euler(pos.eulerAngles + new Vector3(0, -90, 0));
        gameObject.SetActive(true);
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
