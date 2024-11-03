using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerArrow : MonoBehaviour
{
    public static ManagerArrow Ins;
    [SerializeField] private List <ArrowWeapon> ListArrows;

    private ArrowWeapon arrowWeapon;
    private void Awake()
    {
        Ins = this;
        arrowWeapon = ListArrows[0];
    }
    public void SetArrow(float dmg, Transform target, string tagAttack,Transform pos)
    {
        
        foreach (ArrowWeapon arrow in GetArrow())
        {
            arrow.SetDataArrow(dmg, target, tagAttack, pos);
            return;
        }

        ArrowWeapon ar = Instantiate(arrowWeapon, transform);
        ar.gameObject.SetActive(false);
        ListArrows.Add(ar);
        ar.SetDataArrow(dmg, target, tagAttack, pos);
    }

    private IEnumerable<ArrowWeapon> GetArrow()
    {
        foreach (ArrowWeapon g in ListArrows)
        {
            if(!g.gameObject.activeSelf)
            {
                yield return g;

            }
        }
    }
}
