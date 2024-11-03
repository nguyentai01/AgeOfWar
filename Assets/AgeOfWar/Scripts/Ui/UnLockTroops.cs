using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnLockTroops : MonoBehaviour
{
    private int[] ArrayTroopLocks;
    private List<int> ListTroopCurrents = new List<int>();

    [SerializeField] private GameObject[] ButtonUnLocks;
    [SerializeField] private TextMeshProUGUI[] CoinlockTxt;
    [SerializeField] private TextMeshProUGUI[] NameEnenys;
    [SerializeField] private Button[] ButtonUnlockUis;
    [SerializeField] private GameObject[] ListTroop1s;
    [SerializeField] private GameObject[] ListTroop2s;
    [SerializeField] private GameObject[] ListTroop3s;

    private Listtrop[] Listtrop;
    private float coin;
    private int age;
    private void OnEnable()
    {
        GetDataLock();
        SetTurnOn();
    }

    private void GetDataLock()
    {
        ListTroopCurrents.Clear();
        Listtrop = LoadResource.Ins.planetData.Plant[0].ListTroops;
        ArrayTroopLocks = (int[])Pref.GetArrayDataUnlockTroops().Clone();
        GetData();
        for (int i = age * 3; i < (age + 1) * 3; i++)
        {
            ListTroopCurrents.Add(ArrayTroopLocks[i]);

        }
        CheckCoin();
    }

    private void GetData()
    {
        age = Pref.GetAgePlay();
        coin = Pref.GetCoin();
    }

    public void CheckCoin()
    {
        for (int i = 0; i < ListTroopCurrents.Count; i++)
        {
            if (ListTroopCurrents[i] <= 0)
            {
                ButtonUnLocks[i].SetActive(true);
                CoinlockTxt[i].text = HelperFunc.ConvertValue(Listtrop[age].InforTroopPlayer[i].Lock) + "";
                NameEnenys[i].gameObject.SetActive(false);

            }
            else
            {
                ButtonUnLocks[i].SetActive(false);
                NameEnenys[i].text = Listtrop[age].InforTroopPlayer[i].NameTroop;
                NameEnenys[i].gameObject.SetActive(true);
            }
        }

        EventUi.Ins.CheckButtonByCoin();
    }

    public void CheckButtonByCoin()
    {
        for (int i = 0; i < ListTroopCurrents.Count; i++)
        {
            ButtonUnlockUis[i].interactable = Listtrop[age].InforTroopPlayer[i].Lock <= coin;
        }
    }
    public void Btn_UnLock(int id)
    {
        if (Pref.GetCoin() < Listtrop[age].InforTroopPlayer[id].Lock)
        {
            CheckCoin();
            return;
        }
        ListTroopCurrents[id] = 1;
        ArrayTroopLocks[age * 3 + id] = 1;
        Pref.SetDataUnlockTroop(ArrayTroopLocks);
        GameController.Ins.SetCoin(-Listtrop[age].InforTroopPlayer[id].Lock);
        GetData();
        CheckCoin();
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_Click);
    }

    private void SetTurnOn()
    {
        for (int i = 0; i < ListTroop1s.Length; i++)
        {
            ListTroop1s[i].SetActive(i == age);
            ListTroop2s[i].SetActive(i == age);
            ListTroop3s[i].SetActive(i == age);

        }
    }
}
