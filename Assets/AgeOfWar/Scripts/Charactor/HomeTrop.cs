using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTrop : MonoBehaviour
{
    private float HpDefault = 10;
    private int currentAge;
    private PlantHome PlantUpdateHome;
    private List<TroopHome> ListLevelHomes;
    public LayerEneny NameBotPlayer = LayerEneny.EnenyPlayer;

    [SerializeField] private EnenyController Controller;
    [SerializeField] private GameObject [] Barracks;
    [SerializeField] private GameObject effect;
    private void Start()
    {
        //    PlantUpdateHome = LoadResource.Ins.PlantUpdateHome;
        SetCurrentBarracks();
    }
    public void SetCurrentBarracks()
    {
        ListLevelHomes = LoadResource.Ins.GetListLevelHomes(Pref.GetplantPlay());
        if ((int)NameBotPlayer == 1)
        {
            //player
            HpDefault = (ListLevelHomes[Pref.GetAgePlay()].HpHomePlay * Mathf.Pow(LoadResource.Ins.DataGame.HpPerStep, Pref.GetLevelHpHome())) + Pref.GetLevelHpHome();
            SetBarracks(Pref.GetAgePlay());
        }
        else
        {
            //Bot

            HpDefault = ListLevelHomes[Pref.GetAgeBot()].HpHomeBot;
            SetBarracks(Pref.GetAgeBot());

        }
        Controller.ResetEney(99, HpDefault);
        SetLayer();
        UiManger.Ins.UpdateTxtHp();
    }
    private void SetBarracks(int Age)
    {
        currentAge = Age;
        for (int i = 0; i< Barracks.Length;i++)
        {
            Barracks[i].SetActive(i == Age);
        }
    }
    public void UpdateHp(float Hp)
    {
        HpDefault += Hp;
        Controller.UpdateHp(Hp);

    }
    private void SetLayer()
    {
       string TagEneny = LayerEneny.GetName(typeof(LayerEneny), NameBotPlayer);
        gameObject.layer = LayerMask.NameToLayer(TagEneny);
        gameObject.tag = TagEneny;
    }

    public float GetHpHome()
    {
        return HpDefault;
    }

    public void Losed()
    {
        if (!effect.activeSelf)
        {
            effect.SetActive(true);
        }
        GameController.Ins.SnakeCam();
        Barracks[currentAge].transform.DOScaleZ(1,.5f);
    }
}
