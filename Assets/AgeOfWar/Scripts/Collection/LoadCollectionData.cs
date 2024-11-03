using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;

[Serializable]
public class Colection
{
    public string nameIcon;
    public string nameItem;
    public int baseBonus;
    public int rare;
    public int rate;
    public float bonusPercentLevel;
}

[System.Serializable]
public class ColectionData
{
    public List<Colection> collectionData;
}
[Serializable]
public class InfoCollectionPlayer
{
    public int idBonus;
    public int rare;
    public int level;
    public int amount;
}
[Serializable]
public class DataCollectionPlayer
{
    public List<InfoCollectionPlayer> infoCollectionPlayers;
}
public class LoadCollectionData : MonoBehaviour
{
    public static LoadCollectionData ins;
    public TextAsset txtData;
    public DataCollectionPlayer dataCollectionPlayer;
    public List<ColectionData> dataCollection;

    public float totalBonusDmg, totalBonusHeath, totalBonusFood, totalBonusCoin;
    // Start is called before the first frame update
    void Start()
    {
        if (ins == null)
        {
            ins = this;
            DontDestroyOnLoad(gameObject);
        }
        LoadData();
        LoadSaveDataCollectionPlayer(true);
        EventUpdateTotalBonus();
    }

    public void EventUpdateTotalBonus()
    {
        totalBonusDmg = totalBonusHeath = totalBonusFood = totalBonusCoin = 0;
        for (int i=0;i< dataCollectionPlayer.infoCollectionPlayers.Count; i++)
        {
            Colection colection = GetColection(dataCollectionPlayer.infoCollectionPlayers[i].idBonus, dataCollectionPlayer.infoCollectionPlayers[i].rare);

            switch (dataCollectionPlayer.infoCollectionPlayers[i].idBonus)
            {
                case 0:
                    totalBonusDmg +=(colection.baseBonus+(colection.bonusPercentLevel* (dataCollectionPlayer.infoCollectionPlayers[i].level-1)));
                    break;
                case 1:
                    totalBonusHeath += (colection.baseBonus + (colection.bonusPercentLevel * (dataCollectionPlayer.infoCollectionPlayers[i].level - 1)));
                    break;
                case 2:
                    totalBonusFood += (colection.baseBonus + (colection.bonusPercentLevel * (dataCollectionPlayer.infoCollectionPlayers[i].level - 1)));
                    break;
                case 3:
                    totalBonusCoin += (colection.baseBonus + (colection.bonusPercentLevel * (dataCollectionPlayer.infoCollectionPlayers[i].level - 1)));
                    break;
            }
        }
    }

    void LoadData()
    {
        dataCollection = JsonConvert.DeserializeObject<List<ColectionData>>(txtData.text);
    }

    public string pathData;
    public void LoadSaveDataCollectionPlayer(bool _isLoad)
    {
        pathData = Path.Combine(Application.persistentDataPath, "dataCollection.dat");
        if (_isLoad)
        {
            if (File.Exists(pathData))
            {
                string data = File.ReadAllText(pathData);
                dataCollectionPlayer = JsonUtility.FromJson<DataCollectionPlayer>(data);
            }
            else
            {
                dataCollectionPlayer = new DataCollectionPlayer();
                dataCollectionPlayer.infoCollectionPlayers = new List<InfoCollectionPlayer>();
                File.WriteAllText(pathData, JsonConvert.SerializeObject(dataCollectionPlayer));
            }
        }
        else
        {
            File.WriteAllText(pathData, JsonConvert.SerializeObject(dataCollectionPlayer));
        }

    }

    public void UpdateCollectionPlayer(Colection _newColection, int _type)
    {

        if (dataCollectionPlayer.infoCollectionPlayers==null)
        {
            dataCollectionPlayer.infoCollectionPlayers = new List<InfoCollectionPlayer>();
            CreatInfoCollectionPlayer(_newColection, _type);
        }
        else
        {
            for (int i = 0; i < dataCollectionPlayer.infoCollectionPlayers.Count; i++)
            {
                if (dataCollectionPlayer.infoCollectionPlayers[i].idBonus == _type)
                {
                    if (dataCollectionPlayer.infoCollectionPlayers[i].rare == _newColection.rare)
                    {
                        InfoCollectionPlayer infoCollectionPlayer = dataCollectionPlayer.infoCollectionPlayers[i];
                        infoCollectionPlayer.amount = dataCollectionPlayer.infoCollectionPlayers[i].amount + 1;
                        dataCollectionPlayer.infoCollectionPlayers[i] = infoCollectionPlayer;
                        LoadSaveDataCollectionPlayer(false);
                        return;
                    }
                }
            }
            CreatInfoCollectionPlayer(_newColection, _type);
        }
    }

    void CreatInfoCollectionPlayer(Colection _newColection, int _type)
    {
        InfoCollectionPlayer infoCollectionPlayer = new InfoCollectionPlayer();
        infoCollectionPlayer.idBonus = _type;
        infoCollectionPlayer.level = 1;
        infoCollectionPlayer.rare = _newColection.rare;
        infoCollectionPlayer.amount = 1;
        dataCollectionPlayer.infoCollectionPlayers.Add(infoCollectionPlayer);

        switch (infoCollectionPlayer.idBonus)
        {
            case 0:
                totalBonusDmg += _newColection.baseBonus;
                break;
            case 1:
                totalBonusHeath += _newColection.baseBonus;
                break;
            case 2:
                totalBonusFood += _newColection.baseBonus;
                break;
            case 3:
                totalBonusCoin += _newColection.baseBonus;
                break;
        }
        LoadSaveDataCollectionPlayer(false);
    }

    public void EventUpgrade(InfoCollectionPlayer _infoCollectionPlayer, int _type)
    {
        for (int i = 0; i < dataCollectionPlayer.infoCollectionPlayers.Count; i++)
        {
            if (dataCollectionPlayer.infoCollectionPlayers[i].idBonus == _type)
            {
                if (dataCollectionPlayer.infoCollectionPlayers[i].rare == _infoCollectionPlayer.rare)
                {
                    InfoCollectionPlayer infoCollectionPlayer = dataCollectionPlayer.infoCollectionPlayers[i];
                    infoCollectionPlayer.amount = dataCollectionPlayer.infoCollectionPlayers[i].amount - (int)Mathf.Pow(2, infoCollectionPlayer.level - 1);
                    infoCollectionPlayer.level++;
                    dataCollectionPlayer.infoCollectionPlayers[i] = infoCollectionPlayer;

                    switch (dataCollectionPlayer.infoCollectionPlayers[i].idBonus)
                    {
                        case 0:
                            totalBonusDmg += GetColection(_infoCollectionPlayer.idBonus, _infoCollectionPlayer.rare).bonusPercentLevel;
                            break;
                        case 1:
                            totalBonusHeath += GetColection(_infoCollectionPlayer.idBonus, _infoCollectionPlayer.rare).bonusPercentLevel;
                            break;
                        case 2:
                            totalBonusFood += GetColection(_infoCollectionPlayer.idBonus, _infoCollectionPlayer.rare).bonusPercentLevel;
                            break;
                        case 3:
                            totalBonusCoin += GetColection(_infoCollectionPlayer.idBonus, _infoCollectionPlayer.rare).bonusPercentLevel;
                            break;
                    }
                    LoadSaveDataCollectionPlayer(false);
                    return;
                }
            }
        }
    }

    public Colection GetColection(int _idType,int _rare)
    {
        foreach (Colection colection in dataCollection[_idType].collectionData)
        {
            if (colection.rare == _rare)
            {
                return colection;
            }
        }
        return null;
    }
}
