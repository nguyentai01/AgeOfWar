using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase.Extensions;
using System;

public class ConfigFireBaseRemote : MonoBehaviour
{
    Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
           // ManagerAds.ins.timeShowAds = 30f;
        }
        else
        {
            Invoke("DelayGetData", 2f);
        }
    }

    void DelayGetData()
    {
        FetchDataAsync();
    }

    void InitializeFirebase()
    {
        Debug.Log("FireBase Ready");
    }

    public void ShowData()
    {
       //long time = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("Time_Show_Ads").LongValue;
        long ads_interval = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("ads_interval").LongValue;
        string DataGame = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("DataGame").StringValue;
        string DataTroop = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("DataTroop").StringValue;
        string DataUpdate = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("DataUpdate").StringValue;
        string DataWave = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("DataWave").StringValue;


      //  Debug.Log("Time_Show_Ads: " + time);
        Debug.Log("ads_interval: " + ads_interval);
        Debug.Log("DataGame: " + DataGame);
        Debug.Log("DataTroop: " + DataTroop);
        Debug.Log("DataUpdate: " + DataUpdate);
        Debug.Log("DataWave: " + DataWave);

        InitAds.ins.GetData(ads_interval, DataTroop, DataWave, DataUpdate, DataGame);

    }

    public Task FetchDataAsync()
    {
        Debug.Log("Fetching data...");
        System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);
    }
    //[END fetch_async]

    void FetchComplete(Task fetchTask)
    {
        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus)
        {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                .ContinueWithOnMainThread(task =>
                {
                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                   info.FetchTime));
                });

                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        Debug.Log("Fetch failed for unknown reason");
                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                        break;
                }
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                Debug.Log("Latest Fetch call still pending.");
                break;
        }

        if (fetchTask.IsCanceled)
        {
            Debug.Log("Fetch canceled.");
        }
        else if (fetchTask.IsFaulted)
        {
            Debug.Log("Fetch encountered an error.");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.Log("Fetch completed successfully!");
            ShowData();
        }
    }
}
