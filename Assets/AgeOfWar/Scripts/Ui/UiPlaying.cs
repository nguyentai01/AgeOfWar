using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiPlaying : MonoBehaviour
{
    [SerializeField] private GameObject[] ListTroops;
    [SerializeField] private GameObject[] Infs;
    [SerializeField] private GameObject[] Range;
    [SerializeField] private GameObject[] Cav;
    [SerializeField] private Transform[] parrentOb;
    [SerializeField] private Image[] listBgTroops;
    [SerializeField] private TextMeshProUGUI[] listTextTroops;
    [SerializeField] private Sprite[] bgTroops;
    [SerializeField] private TextMeshProUGUI getFoodRewardTxt;

    private List<int> ListTroopCurrents = new List<int>();
    private List<GameObject> ListTroopModels = new List<GameObject>();
    private int[] ArrayTroopLocks;
    private int age;
    private float coin;

    private void OnEnable()
    {
        GetDataLock();
        InitTroops();
        int food = LoadResource.Ins.planetData.Plant[Pref.GetplantPlay()].ListTroops[Pref.GetAgePlay()].FoodReward;
        getFoodRewardTxt.text = "" + food;
    }

    private void GetDataLock()
    {
        ListTroopCurrents.Clear();
        ArrayTroopLocks = (int[])Pref.GetArrayDataUnlockTroops().Clone();
        GetData();
        for (int i = age * 3; i < (age + 1) * 3; i++)
        {
            ListTroopCurrents.Add(ArrayTroopLocks[i]);
        }
        for(int i=0;i< ListTroops.Length;i++)
        {
            if(ListTroopCurrents[i]>0)
            {
                ListTroops[i].SetActive(true);
            }
            else
            {
                ListTroops[i].SetActive(false);

            }
        }
    }

    private void InitTroops()
    {
        if(age>= Infs.Length)
        {
            return;
        }
        GameObject g = Instantiate(Infs[age], parrentOb[0]);
        g.transform.localRotation = Quaternion.Euler(-40, 0, 0);
       // g.transform.position = Vector3.zero;
        ListTroopModels.Add(g);

        g = Instantiate(Range[age], parrentOb[1]);
        g.transform.localRotation = Quaternion.Euler(-40, 0, 0);
     //   g.transform.position = Vector3.zero;
        ListTroopModels.Add(g);

        g = Instantiate(Cav[age], parrentOb[2]);
        g.transform.localRotation = Quaternion.Euler(-40, 0, 0);
     //   g.transform.position = Vector3.zero;
        ListTroopModels.Add(g);
    }

    public void CheckFoodUi(List<int> arrayFood,int currentFood)
    {
        for(int i=0;i< arrayFood .Count;i++)
        {
            if(arrayFood[i] <= currentFood)
            {
                listBgTroops[i].sprite = bgTroops[0];
                listTextTroops[i].color = Color.white;
            }
            else
            {
                listBgTroops[i].sprite = bgTroops[1];
                listTextTroops[i].color = Color.red;
            }

        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < ListTroopModels.Count; i++)
        {
            Destroy(ListTroopModels[i]);
        }

        ListTroopModels.Clear();
    }

    private void GetData()
    {
        age = Pref.GetAgePlay();
        coin = Pref.GetCoin();
    }

    public void SetOffMoldePlaying(bool status)
    {
        for (int i = 0; i < parrentOb.Length; i++)
        {
            parrentOb[i].gameObject.SetActive(status);
        }
    }
}
