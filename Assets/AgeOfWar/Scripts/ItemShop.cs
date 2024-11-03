using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] bool isVideo;
    [SerializeField] string productId;
    [SerializeField] int amountRuby;
    [SerializeField] TextMeshProUGUI txtPrice, txtRuby;
    [SerializeField] bool isRemoveAds;
    [SerializeField] GameObject iconRemoveAds;
    private IStoreController controller;
    int amountVideoWatched;
    private void Start()
    {
        txtRuby.text = HelperFunc.ConvertValue(amountRuby);
        if (isVideo)
        {
            if (Pref.GetDaySaveShop() != DateTime.Now.DayOfYear)
            {
                amountVideoWatched = 0;
                Pref.SetCountVideo(0);
                Pref.SetDaySaveShop();
            }
            else
            {
                amountVideoWatched = Pref.GetCountVideo();
            }

            if (amountVideoWatched >= 5)
            {
                GetComponent<Button>().interactable = false;
            }
            if (Pref.GetStateTut("Tut05") == 0)
            {
                txtPrice.text = "Free";
                UiManger.Ins.tutorialController.itemShop = this;
            }
            else
            {
                UpdateTextVideo();
            }
        }

        if (isVideo) return;
        var product = CodelessIAPStoreListener.Instance.GetProduct(productId);
        txtPrice.text = product.metadata.localizedPriceString;
        if (isRemoveAds)
        {
            iconRemoveAds.SetActive(Pref.GetRemoveAds() == 0);
        }
    }
    public void EventBuyVideo()
    {
        InitAds.ins.ShowRewarded((x) =>
        {
            if (x)
            {
                amountVideoWatched++;
                if (amountVideoWatched >= 5)
                {
                    GetComponent<Button>().interactable = false;
                }
                UpdateTextVideo();
                Pref.SetCountVideo(amountVideoWatched);
                Pref.SetRuby(amountRuby);
                UiManger.Ins.GetRuby();
            }
        });
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);

    }

    public void EventComplete()
    {
        Debug.Log("BUY" + amountRuby);
        Pref.SetRuby(amountRuby);
        UiManger.Ins.GetRuby();
        if (isRemoveAds)
        {
            EventUi.Ins.EventCompleteBuyRemoveAds();
        }
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click, 1);

    }

    public void UpdateTextVideo()
    {
        txtPrice.text = amountVideoWatched + "/" + 5;
    }

    public void EventFail()
    {
        UiManger.Ins.EventFailPay();
    }
}
