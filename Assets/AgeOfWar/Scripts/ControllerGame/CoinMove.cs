using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Rigidbody body;

    private Vector3 Target2;

    public int Coin = 0;
    private void OnEnable()
    {
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        // body.isKinematic = false;
        yield return transform.DOJump(Target2,1,1,1f).WaitForCompletion();
        yield return new WaitForSeconds(LoadResource.Ins.DataGame.TimeCoinMove);
       // body.isKinematic = true;
       // transform.DOScale(new Vector3(.07f, .07f, .07f), 1);
      //  yield return transform.DOMove(Target.position, 1).WaitForCompletion();
      
        GameController.Ins.SetCoin(Coin);
        gameObject.SetActive(false);
    }
    public void SetCoin(int coin,Vector3 Target,Vector3 target2)
    {
        Coin = coin;
        transform.position = Target;
        this.Target2 = target2;
        gameObject.SetActive(true);

    }
}
