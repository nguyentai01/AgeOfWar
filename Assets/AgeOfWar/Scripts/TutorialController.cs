using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class InfoTutorial
{
    public string idTut;
    public int numberOfPlay;
    public GameObject tut;
}

public class TutorialController : MonoBehaviour
{
    public List<InfoTutorial> infoTutorials;
    InfoTutorial tutNow;
    public ItemShop itemShop;

    private void Start()
    {
        ShowTut("Tut02");
        ShowTut("Tut04");
        if (Pref.GetStateTut("Tut05") != 0)
        {
            ShowTut("Tut06");
        }
    }

    public void ShowTut(string _idTut)
    {
        int numberOfPlay = Pref.GetNumberOfPlay();
        for (int i = 0; i < infoTutorials.Count; i++)
        {
            if (_idTut == infoTutorials[i].idTut)
            {
                if (Pref.GetStateTut(_idTut) == 0)
                {
                    if (numberOfPlay == infoTutorials[i].numberOfPlay)
                    {
                        infoTutorials[i].tut.SetActive(true);
                        tutNow = infoTutorials[i];
                    }
                }
            }
        }
    }

    public void HideTut()
    {
        if (tutNow != null)
        {
            Pref.SetStateTut(tutNow.idTut);
            tutNow.tut.SetActive(false);
            tutNow = null;
            Time.timeScale = 1;
        }
    }

    public void OnclickTut05()
    {
        HideTut();
        itemShop.EventComplete();
        itemShop.UpdateTextVideo();
        ShowTut("Tut06");
    }

    public void OnclickTut07()
    {
        HideTut();
        FindObjectOfType<PanelCollectionController>().EventGacha(1);
    }
}
