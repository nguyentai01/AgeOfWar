using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EnenyController : MonoBehaviour
{
    public void RunAttack(Transform TargetAttack, bool status)
    {
        Attack = status;
        this.TargetAttack = TargetAttack;
        EnenyAttack = TargetAttack.GetComponent<EnenyController>();
        EnenyAttack.AddEnenyAttack(FinishAttack, status);
        switch ((int)TypeTroop)
        {
            case 1:
            case 3:
                if (Attack)
                {

                    StartCoroutine(StartAttack());
                }
                else
                {
                    StopAllCoroutines();
                    // EnenyMove.ContinueMove();
                    EnenyAttack = null;
                }
                break;
            case 2:
                if (Attack)
                {
                    StartCoroutine(AttackRanger());
                }
                break;
        }
    }
    public void SetPause(bool pause)
    {
        IsPause = pause;
        EnenyMove.PauseGame(pause);
        ControlEneny.SetPause(pause);

        if (pause)
        {
            StopAllCoroutines();
        }
        else
        {
            switch ((int)TypeTroop)
            {
                case 1:
                case 3:
                    if (Attack)
                    {
                        StartCoroutine(StartAttack());
                    }
                    else
                    {
                        StopAllCoroutines();
                        // EnenyMove.ContinueMove();
                        EnenyAttack = null;
                    }
                    break;
                case 2:
                    if (Attack)
                    {
                        StartCoroutine(AttackRanger());
                    }
                    break;
            }
        }
    }
}
