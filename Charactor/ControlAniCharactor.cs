using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAniCharactor : MonoBehaviour
{
    [SerializeField] private Animator Ani;
    public void SetAni(TypeAnim TypeAnim)
    {
        Ani.SetInteger(ConstName.status, (int)TypeAnim);
    }
}
public enum TypeAnim
{
    Walk =0,
    AttackDefault = 0,
    Win = 0,

}