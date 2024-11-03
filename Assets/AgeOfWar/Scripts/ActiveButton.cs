using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    [SerializeField] int numberOfPlay;

    private void OnEnable()
    {
        if(Pref.GetNumberOfPlay()< numberOfPlay)
        {
            gameObject.SetActive(false);
        }
    }
}
