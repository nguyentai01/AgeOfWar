using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    [SerializeField] private Image LoadImg;
    private AsyncOperation asyncLoad;
    public int TimeLoad = 6;
    private void Start()
    {
        Invoke("ShowOpenAds", TimeLoad - 1);
        Loading();
    }

    private void Loading()
    {
        LoadImg.fillAmount = 0;
        asyncLoad = SceneManager.LoadSceneAsync(ConstName.Playing);
        asyncLoad.allowSceneActivation = false;
        Pref.SetWinLv(0);
        DOTween.To(() => LoadImg.fillAmount, x => LoadImg.fillAmount = x, 1, TimeLoad).OnComplete(() =>
        {
            asyncLoad.allowSceneActivation = true;

        });
    }

    private void ShowOpenAds()
    {
        AdmobInit.ins.ShowAppOpenAds();
        InitAds.ins.ShowBanner();
    }
}
