using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Ins { get; private set; }
    private List<CoinMove> ListCoinObjects = new List<CoinMove>();

    [SerializeField] private CoinMove coin;
    [SerializeField] private Renderer ren;
    private void Awake()
    {
        Ins = this;
    }

    public void SetCoin(int coin, Vector3 Target)
    {
       
        foreach (CoinMove currentCoin in GetEneny())
        {
            currentCoin.SetCoin(coin, Target, GetPosRandom());
            Pref.SetCoinInGame(coin);
            AudioManager.Ins.SetSfx(AudioManager.Ins.snd_coin_drop);
            return;
        }

        CoinMove coinIn = Instantiate(this.coin, transform);
        coinIn.SetCoin(coin, Target, GetPosRandom());
        ListCoinObjects.Add(coinIn);
        Pref.SetCoinInGame(coin);
        AudioManager.Ins.SetSfx(AudioManager.Ins.snd_coin_drop);
    }

    private IEnumerable<CoinMove> GetEneny()
    {
        foreach (CoinMove coin in ListCoinObjects)
        {
            if (!coin.gameObject.activeSelf)
            {
                yield return coin;
            }
        }
    }

    private Vector3 GetPosRandom()
    {
        Bounds bound = ren.bounds;

        return new Vector3(Random.Range(bound.min.x, bound.max.x),
            Random.Range(bound.min.y, bound.max.y),
            Random.Range(bound.min.z, bound.max.z));
    }
}
