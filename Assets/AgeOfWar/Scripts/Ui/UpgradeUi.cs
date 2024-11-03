using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    [SerializeField] private GameObject[] UpgradesUi;
    [SerializeField] private GameObject[] SelectUpgrades;
    [SerializeField] private TextMeshProUGUI[] TextUpgrades;
    [SerializeField] private Color ColorSelect;
    [SerializeField] private Button[] ListButtonUpgrades;
    [SerializeField] private TextMeshProUGUI[] TextAges;
    [SerializeField] private GameObject[] Age1;
    [SerializeField] private GameObject[] Age2;
    [SerializeField] private GameObject AgeContent;

    private void OnEnable()
    {
        CheckMaxLv();
    }
    public void Btn_SelectUpgrade(int index)
    {
        for (int i = 0; i < UpgradesUi.Length; i++)
        {
            UpgradesUi[i].SetActive(i == index);
            SelectUpgrades[i].SetActive(i == index);
            SetColorTextUpgrade(i, i == index);
        }
        if (index == 1)
        {
            UiManger.Ins.SetTitleHeader(5);
            CheckMaxLv();
            if (Pref.GetAgePlay() < 5)
            {
                SetContentAgeUi();

            }
        }
        else
        {
            UiManger.Ins.SetTitleHeader(1);
        }
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);
    }

    private void CheckMaxLv()
    {
        if(Pref.GetAgePlay()==5)
        {
            AgeContent.SetActive(false);
            SetOffButtonUpdate(2, false);
        }
    }

    public void SetContentAgeUi()
    {
        int CurrentAge = Pref.GetAgePlay();
        TextAges[0].text ="Age " + (CurrentAge+1);
        TextAges[1].text = "Age "+ (CurrentAge + 2);
        for (int i = 0; i < 5; i++)
        {
            Age1[i].SetActive(i == CurrentAge);

        }
        for (int i = 0; i < 6; i++)
        {
            Age2[i].SetActive(i == CurrentAge + 1);

        }
    }
    private void SetColorTextUpgrade(int index, bool status)
    {
        if (status)
        {
            TextUpgrades[index].color = ColorSelect;

        }
        else
        {
            TextUpgrades[index].color = Color.white;
        }
    }
    public void Btn_UpdateAge()
    {

    }
    public void SetEnableButton(int index, bool status)
    {
        ListButtonUpgrades[index].interactable = status;
    }
    public void SetOffButtonUpdate(int index, bool status)
    {
        ListButtonUpgrades[index].gameObject.SetActive(status);

    }
}
