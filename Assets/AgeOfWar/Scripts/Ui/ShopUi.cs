using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUi : MonoBehaviour
{
    [SerializeField] private GameObject iconReward;

    private void OnEnable()
    {

        iconReward.SetActive(Pref.GetStateTut("Tut05") == 1);

    }
}
