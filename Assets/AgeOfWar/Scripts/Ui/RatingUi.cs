using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatingUi : MonoBehaviour
{


    public void Btn_Rating(int rate)
    {
        if(rate<1)
        {
            Pref.SetWinLv(0);

            gameObject.SetActive(false);
            // 1- 4 star
        }
        else
        {
            Pref.SetRating();
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.warrior.squad.warrior.battle&fbclid=IwAR2iBlXomiXqX2GqkLXIe7avuHCZY8mMUsPAuotWM68pGSVgE1Va_xjKzvQ_aem_AWdatfuUijBjyuOSaQgFCmDQHu5qszzV9E8rpwOEMq-0W114qZRzdxROXtsvqXcNLd2ACfIAHk6L6WnMZLm3G1Wg");
            gameObject.SetActive(false);

        }
    }
}
