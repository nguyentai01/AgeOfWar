using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUi : MonoBehaviour
{
    [SerializeField] private GameObject[] NextGames;
    [SerializeField] private HomeTrop Bot;
    [SerializeField] private GameObject NextLv;
    [SerializeField] private TextMeshProUGUI battleTxt;
    private int ageCount = 0;

    private void OnEnable()
    {
        ageCount = Pref.GetCountLvUnlock();
        SetUiNextLevel();
    }


    private void SetUiNextLevel()
    {
        if (ageCount == 0)
        {
            NextGames[0].SetActive(false);
            NextGames[1].SetActive(false);

        }
        else if (Pref.GetAgeBot() == 0)
        {
            NextGames[0].SetActive(false);
            NextGames[1].SetActive(true);

        }
        else if (Pref.GetAgeBot() == ageCount)
        {
            NextGames[1].SetActive(false);
            NextGames[0].SetActive(true);
        }
        else
        {
            NextGames[1].SetActive(true);
            NextGames[0].SetActive(true);
        }
    }
    public void SetNextAge(int age)
    {

        Pref.SetAgeBot(age);
        UiManger.Ins.SetTitleHeader(2);
        SetUiNextLevel();
        GameController.Ins.SetMap(Pref.GetAgeBot());
        Bot.SetCurrentBarracks();
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);
    }

    public void SetOff(bool status)
    {
        NextLv.SetActive(status);
    }
    
    public void SetResume(bool status)
    {
        if (status)
        {
            battleTxt.text = "RESUME";

        }
        else
        {
            battleTxt.text = "BATTLE";

        }
    }
}
