using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProccesFood : MonoBehaviour
{
    private float FoodGet = 0;
    private Image FoodImg;
    private bool IsPause = false,isClick = false;
    private IEnumerator Ie_Food;
    private DataGame DataGame;
    private int FoodCurrent = 0;


    private void Start()
    {
        DataGame = LoadResource.Ins.DataGame;

        FoodImg = UiManger.Ins.FoodImg;


        Ie_Food = StartGetFood();
        //Start_IE_Food();
        
    }
    private void Start_IE_Food()
    {
        StartCoroutine(Ie_Food);
    }
    private void Stop_IE_Food()
    {
        StopCoroutine(Ie_Food);
    }
    private IEnumerator StartGetFood()
    {
        SetFoodGetByLv();
        UiManger.Ins.SetFoodTxt(FoodCurrent);
        while (!IsPause)
        {
            FoodImg.fillAmount = 0;
            yield return DOTween.To(() => FoodImg.fillAmount, x => FoodImg.fillAmount = x, 1, (1f/ FoodGet)).SetEase(Ease.Linear).WaitForCompletion();
            //   FoodCurrent += FoodGet;
            SetFood(1);

            UiManger.Ins.SetFoodTxt(FoodCurrent);
        }
    }
    public void SetFoodGetByLv()
    {
        FoodGet = (Pref.GetLevelUpdateFood() * DataGame.FoodPerStep) + DataGame.FoodDefault;
        FoodGet += ((FoodGet * LoadCollectionData.ins.totalBonusFood) / 100f);
    }
    public void GetFoodClick(int Food)
    {
        if (!IsPause)
        {
            SetFood(Food);
            UiManger.Ins.SetFoodTxt(FoodCurrent);
        }

    }
    public void PauseGame(bool pause)
    {
        IsPause = pause;

        if (!IsPause)
        {
            Start_IE_Food();
        }
        else
        {
            Stop_IE_Food();
        }
    }
    public bool GetFood(int Food)
    {
        if (Food <= FoodCurrent)
        {
            SetFood(-Food);
            UiManger.Ins.SetFoodTxt(FoodCurrent);
            return true;
        }
        return false;
    }

    public int GetFood()
    {
        return FoodCurrent;
    }

    private void SetFood(int food)
    {
        FoodCurrent += food;
        EventUi.Ins.CheckSpawFood();
    }

    public void OnPoinDown()
    {
        if (isClick)
        {
            return;
        }
        isClick = true;
        GetFoodClick(1);
        EventUi.Ins.CheckSpawFood();
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);

    }
    public void OnPoinUp()
    {
        isClick = false;

    }
}
