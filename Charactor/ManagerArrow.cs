using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerArrow : MonoBehaviour
{
    public static ManagerArrow Ins;
    [SerializeField] private ArrowWeapon[] ListArrows;
    private int IndexArrow = 0;
    private int LengArrow;
    private void Awake()
    {
        Ins = this;
        LengArrow = ListArrows.Length;
    }
    public void SetArrow(int dmg, Transform target, string tagAttack,Transform pos)
    {
        if (ListArrows[IndexArrow].gameObject.activeSelf)
        {
            ListArrows[IndexArrow].gameObject.SetActive(false);
        }
        ListArrows[IndexArrow].SetDataArrow(dmg, target, tagAttack,pos);
        ListArrows[IndexArrow].gameObject.SetActive(true);
        IndexArrow++;
        if (IndexArrow >= LengArrow)
        {
            IndexArrow = 0;
        }
    }
}
