using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniController : MonoBehaviour
{
    [SerializeField] private Animator Ani;
    public EnenyController EnenyControl;
    [SerializeField] private Renderer RenderEneny;
    private void OnEnable()
    {
        Ani.speed = 1;
        if(EnenyControl==null)
        {
            EnenyControl = transform.parent.GetComponent<EnenyController>();
        }
    }

    public void StartAnim(AnimationName Anim, float speed = 1)
    {
        try
        {
            Ani.speed = speed;
            Ani.SetInteger(ConstName.status, (int)Anim);
        }
        catch
        {

        }
    }

    public void SetPause(bool status)
    {
        Ani.speed = status?0:1;
    }
    public void EventAttack()
    {
        if (EnenyControl != null)
        {
            EnenyControl.StartDmgEneny();

        }
        else
        {
            Debug.LogWarning("Dont have EnenyControll");
        }
    }

    public void SetMaterial(Material mat)
    {
        RenderEneny.material = mat;

    }
}
public enum AnimationName
{
    Ani_Move =0,
    Ani_Attack = 1,
    Ani_Die = 2
    
}