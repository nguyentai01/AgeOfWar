using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResource : MonoBehaviour
{
    public static LoadResource Ins;
    public TextAsset DataTropJson;
    public TextAsset DataWayJson;
    public TextAsset DataUpdateJson;
    public TextAsset DataGameJson;

    public planetData planetData;
    public Root PlanetInWayData;
    public PlantHome PlantUpdateHome;
    public DataGame DataGame;
    private void Awake()
    {
        Ins = this;
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        LoadData();
    }
    public void LoadData()
    {
        planetData = JsonUtility.FromJson<planetData>(DataTropJson.ToString());
        PlanetInWayData = JsonUtility.FromJson<Root>(DataWayJson.ToString());
        PlantUpdateHome = JsonUtility.FromJson<PlantHome>(DataUpdateJson.ToString());
        DataGame = JsonUtility.FromJson<DataGame>(DataGameJson.ToString());
    }

    public void LoadDataFromServer(string DataTropJson, string DataWayJson, string DataUpdateJson, string DataGameJson)
    {
        if (!DataTropJson.Equals(""))
        {
            planetData = JsonUtility.FromJson<planetData>(DataTropJson);
        }
        if (!DataWayJson.Equals(""))
        {
            PlanetInWayData = JsonUtility.FromJson<Root>(DataWayJson);
        }
        if (!DataUpdateJson.Equals(""))
        {
            PlantUpdateHome = JsonUtility.FromJson<PlantHome>(DataUpdateJson);
        }
        if (!DataGameJson.Equals(""))
        {
            DataGame = JsonUtility.FromJson<DataGame>(DataGameJson);
        }

        Debug.Log("checkc " + DataGameJson.Equals(""));
        
    }
    public List<TroopHome> GetListLevelHomes(int plant)
    {
        return PlantUpdateHome.PlantHomes[plant].ListLevelHomes;
    }
}
[System.Serializable]
public class planetData
{
    public DataTrop[] Plant;

}

[System.Serializable]
public class DataTrop
{
    public int PlanetName;
    public Listtrop[] ListTroops;
    
}
[System.Serializable]

public class Listtrop
{
    public int Age;
    public int FoodReward;
    public InforTrop[] InforTroopPlayer;
    public InforTrop[] InforTroopBot;
}
[System.Serializable]
public class InforTrop
{
    public string NameTroop;
    public int Hp;
    public int Dmg;
    public int RangerDmg;

    public float Ranger;
    public float Speed;
    public float SpeedAttack;
    public int Food;
    public int Lock;
    public int Coin;

}
[System.Serializable]
public class ListPlanet
{
    public int NamePlanet;
    public List<ListLevel> ListLevel;
}
[System.Serializable]
public class ListLevel
{
    public int Age;
    public List<ListWay> ListWave;
}

[System.Serializable]
public class ListWay
{
    public float TimeDelay;
    public List<int> IdWave;
}
[System.Serializable]
public class Root
{
    public List<ListPlanet> ListPlanets;
}
// Update Hp Home
[System.Serializable]

public class PlantHome
{
    public List<LevelHome> PlantHomes;
}
[System.Serializable]

public class LevelHome
{
    public int IdPlant;
    public List<TroopHome> ListLevelHomes;
}
[System.Serializable]

public class TroopHome
{
    public int Age;
    public int HpHomePlay;
    public int CoinUpdate;
    public int HpHomeBot;
}
//AllData

[System.Serializable]

public class DataGame
{
    public float FoodDefault;
    public float FoodPerStep;
    public float HpPerStep;

    public float TimeCoinMove;
    public float DefaultCoinFoodUpgrade;
    public float DefaultCoinHpUpgrade;

    public float RatioUpgradeFood;
    public float RatioUpgradeHp;

}