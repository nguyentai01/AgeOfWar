using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfoMode
{
    public int numberOfPlay;
    public GameObject lockMode;
}

public class UnlockMode : MonoBehaviour
{
    [SerializeField] List<InfoMode> infoModes;

    private void Start()
    {
        CheckUnLockMode();
    }

    public void CheckUnLockMode()
    {
        int numberOfPlay = Pref.GetNumberOfPlay();
        for (int i = 0; i < infoModes.Count; i++)
        {
            infoModes[i].lockMode.SetActive(infoModes[i].numberOfPlay > numberOfPlay);
        }
    }
}
