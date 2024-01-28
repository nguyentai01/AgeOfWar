using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Ins;
    [SerializeField] private EnenyController[] ListCharactorPlays;
    [SerializeField] private EnenyController[] ListCharactorBot;
    [SerializeField] private Transform[] ListSpaws;
    public Listtrop[] Listtrop;
    
    private void Awake()
    {
        Ins = this;
    }
    private void Start()
    {
        Listtrop = LoadResource.Ins.planetData.Plant[0].Listtrop;
        StartCoroutine(CreateCharactorBot());
    }
    public void CreateCharactorPlayer(int Type =0)
    {
        CreateCharactor(ListCharactorPlays,ListSpaws[0], (TypeTroops)Type);
    }
    private IEnumerator CreateCharactorBot()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            CreateCharactor(ListCharactorBot, ListSpaws[1],(TypeTroops)(Random.Range(1,4)));
        }
    }
    private void CreateCharactor(EnenyController[] ListEneny,Transform Pos,TypeTroops typeTroop)
    {
        for (int i = 0; i < ListEneny.Length; i++)
        {
            if (!ListEneny[i].gameObject.activeSelf)
            {
                ListEneny[i].ResetEney(i, typeTroop, Listtrop[0].InforTrop[(int)typeTroop-1], Pos);
                ListEneny[i].gameObject.SetActive(true);
                break;
            }
        }
    }
}
