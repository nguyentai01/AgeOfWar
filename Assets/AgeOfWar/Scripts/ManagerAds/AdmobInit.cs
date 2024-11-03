
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;
using GoogleMobileAds.Common;

public class AdmobInit : MonoBehaviour
{
    public static AdmobInit ins;

#if UNITY_ANDROID
    [SerializeField] private string appOpenAdUnitID = "ca-app-pub-3940256099942544/3419835294";
#elif UNITY_IOS
    [SerializeField] private string appOpenAdUnitID = "ca-app-pub-3940256099942544/5662855259";
#else
    [SerializeField] private string appOpenAdUnitID = "unexpected_platform";
#endif

    private AppOpenAd appOpenAd;

    private bool isShowingAd = false;
    public  int showAppOpen = 0;

    private void Awake()
    {
        ins = this;
    }

    private void Start()
    {
        showAppOpen = 0;
        // Initialize the Google Mobile Ads SDK.
       
        MobileAds.Initialize(initStatus => { });
        LoadAd();

        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ShowAppOpenAds();
        }
    }
    private IEnumerator ShowAds()
    {
        yield return new WaitForSeconds(4);
        
    }
    private bool IsAdAvailable
    {
        get
        {
            return appOpenAd != null;
        }
    }

    public void LoadAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        
        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(appOpenAdUnitID, ScreenOrientation.Portrait, request, ((_appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                return;
            }

            // App open ad is loaded.
            appOpenAd = _appOpenAd;
        }));
    }

    public void ShowAppOpenAds()
    {
        if (!IsAdAvailable || isShowingAd)
        {
            return;
        }

        appOpenAd.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        appOpenAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        appOpenAd.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        appOpenAd.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        appOpenAd.OnPaidEvent += HandlePaidEvent;

        appOpenAd.Show();
    }
    public bool IsAdvailable()
    {
        if (!IsAdAvailable || isShowingAd)
        {
            return false;
        }
        return true;
    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Closed app open ad");
        // if (!showAppOpen)
        // {
        //     UIManager.ins.ShowUI(MenuUI.GamePlay);
        //     TutorialController.ins.FirstLoad();
        //     GameController.ins.canShowTutorial = true;
        //     showAppOpen = true;
        // }
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        appOpenAd = null;
        isShowingAd = false;
        LoadAd();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        appOpenAd = null;
        LoadAd();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("Displayed app open ad");
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("Received paid event. (currency: {0}, value: {1}", args.AdValue.CurrencyCode, args.AdValue.Value);

        AdValue adValue = args.AdValue;
    }

    private void OnAppStateChanged(AppState state)
    {
        // Display the app open ad when the app is foregrounded.
        UnityEngine.Debug.Log("App State is " + state);
        if (state == AppState.Foreground)
        {
            ShowAppOpenAds();
        }
    }
}