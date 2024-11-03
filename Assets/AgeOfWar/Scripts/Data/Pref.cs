using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Pref
{
   
    //player
    public static void SetAgePlay()
    {
        PlayerPrefs.SetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.AgePlay, GetAgePlay() + 1);
    }
    public static int GetAgePlay()
    {
        var age = PlayerPrefs.GetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.AgePlay, 0);
        return Mathf.Clamp(age, 0, 5);
    }
    public static void SetCountLvUnlock()
    {
        int Age = GetCountLvUnlock() + 1;

        PlayerPrefs.SetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.CountLevelUnlock, Mathf.Clamp(Age, 0, 5));
    }
    public static int GetCountLvUnlock()
    {
        return PlayerPrefs.GetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.CountLevelUnlock, 0);
    }

    //Bot
    public static void SetAgeBot(int age)
    {
        int Age = GetAgeBot() + age;
        PlayerPrefs.SetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.AgeBot, Mathf.Clamp(Age,0,5));
    }
    public static int GetAgeBot()
    {
        var age = PlayerPrefs.GetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.AgeBot, 0);
        return Mathf.Clamp(age, 0, 5);
    }
    public static void SetplantPlay()
    {
        PlayerPrefs.SetInt(ConstName.PlantPlay, GetplantPlay() + 1);
    }
    public static int GetplantPlay()
    {
        return PlayerPrefs.GetInt(ConstName.PlantPlay, 0);
    }

    //Update
    public static void SetLevelUpdateFood()
    {
        PlayerPrefs.SetInt(ConstName.Plant_ + "_" + GetplantPlay()+ConstName.LevelUpdateFood, GetLevelUpdateFood() + 1);
    }
    public static int GetLevelUpdateFood()
    {
        return PlayerPrefs.GetInt(ConstName.Plant_ + "_" + GetplantPlay() + ConstName.LevelUpdateFood, 0);
    }
    public static void SetLevelHpHome()
    {
        PlayerPrefs.SetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.LevelHpHome, GetLevelHpHome() + 1);
    }
    public static int GetLevelHpHome()
    {
        return PlayerPrefs.GetInt(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.LevelHpHome, 0);
    }

    //Coin
    public static void SetCoin(float coin)
    {
        PlayerPrefs.SetFloat(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.CoinAge, GetCoin() + coin);
    }
    public static float GetCoin()
    {
        return PlayerPrefs.GetFloat(ConstName.Plant_ + GetplantPlay() + "_" + ConstName.CoinAge, 0);
    }

    //Ruby
    public static void SetRuby(int ruby)
    {
        PlayerPrefs.SetInt( ConstName.Ruby, GetRuby() + ruby);
    }
    public static int GetRuby()
    {
        return PlayerPrefs.GetInt(ConstName.Ruby, 0);
    }

    //shop
    public static void SetDaySaveShop()
    {
        PlayerPrefs.SetInt(ConstName.dayShop, DateTime.Today.DayOfYear);
    }
    public static int GetDaySaveShop()
    {
        return PlayerPrefs.GetInt(ConstName.dayShop);
    }

    public static void SetCountVideo(int _number)
    {
        PlayerPrefs.SetInt(ConstName.countVideoShop, _number);
    }
    public static int GetCountVideo()
    {
       return  PlayerPrefs.GetInt(ConstName.countVideoShop, 0);
    }

    //DataUnLockTroop
    public static string GetDataUnlockTroop()
    {
        return PlayerPrefs.GetString(ConstName.Plant_ +Pref.GetplantPlay()+ ConstName.LockTroops, "1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0");
    }
    public static void SetDataUnlockTroop(int[] ArrayWeapon)
    {
        string[] result = ArrayWeapon.Select(x => x.ToString()).ToArray();

        string result2 = String.Join(",", result);
        PlayerPrefs.SetString(ConstName.Plant_ + Pref.GetplantPlay() + ConstName.LockTroops, result2);
    }
    public static int[] GetArrayDataUnlockTroops()
    {

        return GetDataUnlockTroop().Split(',').Select(n => Convert.ToInt32(n)).ToArray();
    }

    public static void SetCoinInGame(int coin)
    {
        PlayerPrefs.SetInt(ConstName.CoinInGame, GetCoinInGame()+coin);
    }

    public static void ResetCoinInGame()
    {
        PlayerPrefs.SetInt(ConstName.CoinInGame, 0);
    }

    public static int GetCoinInGame()
    {
        
        return PlayerPrefs.GetInt(ConstName.CoinInGame, 10);

    }

    public static int GetNumberOfPlay()
    {
        return PlayerPrefs.GetInt("NumberOfPlay");
    }

    public static void SetNumberOfPlay()
    {
        int numberOfPlay = GetNumberOfPlay() + 1;
        PlayerPrefs.SetInt("NumberOfPlay", numberOfPlay);
    }

    public static int GetStateTut(string _id)
    {
        return PlayerPrefs.GetInt("StateTut" + _id);
    }

    public static void SetStateTut(string _id)
    {
        PlayerPrefs.SetInt("StateTut" + _id, 1);
    }

    public static int GetLevelCompleted(int _level)
    {
        return PlayerPrefs.GetInt("LevelCompleted" + _level);
    }

    public static void SetLevelCompleted(int _level)
    {
        PlayerPrefs.SetInt("LevelCompleted" + _level, 1);
    }

    public static int GetCountShowInter()
    {
        return PlayerPrefs.GetInt("CountShowInter", 1);
    }

    public static void SetCountShowInter()
    {
        int countInter = GetCountShowInter() + 1;
        PlayerPrefs.SetInt("CountShowInter", countInter);
    }

    //Sfx
    public static void SetSfx(float rang)
    {
        PlayerPrefs.SetFloat(ConstName.Sfx, rang);
    }
    public static float GetSfx()
    {
       return PlayerPrefs.GetFloat(ConstName.Sfx, 1);
    }

    //Music
    public static void SetMusic(float rang)
    {
        PlayerPrefs.SetFloat(ConstName.Music, rang);
    }
    public static float GetMusic()
    {
        return PlayerPrefs.GetFloat(ConstName.Music, 1);
    }

    //Vib
    public static void SetVib(int rang)
    {
        PlayerPrefs.SetInt(ConstName.Vib, rang);
    }
    public static int GetVib()
    {
        return PlayerPrefs.GetInt(ConstName.Vib, 1);
    }

    public static int GetRemoveAds()
    {
        return PlayerPrefs.GetInt("RemoveAds", 0);
    }

    public static void SetRemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
    }

    public static int GetRating()
    {
        return PlayerPrefs.GetInt(ConstName.isRating, 0);
    }

    public static void SetRating()
    {
        PlayerPrefs.SetInt(ConstName.isRating, 1);
    }
    public static int CheckWinLv()
    {
        return PlayerPrefs.GetInt(ConstName.isWinLv, 0);
    }

    public static void SetWinLv(int isWin)
    {
        PlayerPrefs.SetInt(ConstName.isWinLv, isWin);
    }
}
