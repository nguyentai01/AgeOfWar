using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadResource : MonoBehaviour
{
    public static LoadResource Ins;
    public TextAsset ListTrop;
    public planetData planetData;
    private void Awake()
    {
        Ins = this;
    }
    private void OnEnable()
    {
        LoadData();
    }
    public void LoadData()
    {
        planetData = JsonUtility.FromJson<planetData>(ListTrop.ToString());
    }
}
[System.Serializable]
public class planetData
{
    public int PlanetName;
    public DataTrop[] Plant;

}

[System.Serializable]
public class DataTrop
{
    public Listtrop[] Listtrop;
}
[System.Serializable]

public class Listtrop
{
    public InforTrop[] InforTrop;
}
[System.Serializable]
public class InforTrop
{
    public int Hp;
    public int Dmg;
    public float Ranger;
    public float Speed;
    public float SpeedAttack;
    public int Food;

}
/*"DataTrop":
{
    "Listtrop":[

    {
        "InforTrop":[
   
    { "Hp":50,"Dmg":10,"Ranger":10.5,"Speed":5},
	{ "Hp":50,"Dmg":10,"Ranger":10.5,"Speed":5},
	{ "Hp":50,"Dmg":10,"Ranger":10.5,"Speed":5}
	]}
	]
}*/