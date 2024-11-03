using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyManager : MonoBehaviour
{
    public static EnenyManager Ins;
    public EnenyInAge[] EnenyInAges;

    private void Awake()
    {
        Ins = this;
    }
}
[System.Serializable]
public class EnenyInAge
{
    public AniController[] ListEnenys;
}