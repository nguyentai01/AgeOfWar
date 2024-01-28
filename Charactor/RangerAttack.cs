using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerAttack : MonoBehaviour
{
    private string Tag;
    [SerializeField] private EnenyMove eneny;
    private int IdEnenyAttack=0;
    public bool IsAttack = false;
    public int TypeTroop;
    private void OnEnable()
    {
        IsAttack = false;
        IdEnenyAttack = 0;
    }
    public void SetData(string tag)
    {
        Tag = tag;
    }
    public void SetIdEnenyAttack(int IdEneny)
    {
        IsAttack = true;
        IdEnenyAttack = IdEneny;
    }
  /*  private void OnTriggerEnter(Collider other)
    {
        if (!IsAttack)
        {
            if (other.CompareTag(Tag))
            {
                //Attack
                eneny.Attack(other.gameObject.transform, true);
                IdEnenyAttack = other.gameObject.GetComponent<EnenyController>().IdEneny;
                IsAttack = true;
            }
        }
       
    }*/
    private void OnTriggerExit(Collider other)
    {
        if (TypeTroop != 2)
        {
            if (IsAttack)
            {
                if (other.CompareTag(Tag))
                {
                    //Attack
                    if (IdEnenyAttack == other.gameObject.GetComponent<EnenyController>().IdEneny)
                    {
                        eneny.Attack(other.gameObject.transform, false);
                        IsAttack = false;
                    }
                }
            }
        }
        
    }
}
