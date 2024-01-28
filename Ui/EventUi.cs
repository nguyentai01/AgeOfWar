using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventUi : MonoBehaviour
{
    public void Btn_SpamEneny(int TypeTroop =1)
    {
        GameController.Ins.CreateCharactorPlayer(TypeTroop);
    }
}
