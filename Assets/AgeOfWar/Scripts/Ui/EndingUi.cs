using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingUi : MonoBehaviour
{
    [SerializeField] private GameObject WinUi;
    [SerializeField] private GameObject LoseUi;

    public void SetFinishUi(bool status)
    {

        WinUi.SetActive(status);
        LoseUi.SetActive(!status);
       // gameObject.SetActive(true);
    }
}
