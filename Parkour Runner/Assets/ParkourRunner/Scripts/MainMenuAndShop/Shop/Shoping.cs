using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Shoping
{

    public static void GetDonat(string donatsName)
    {
        if (donatsName == "NoAds")
        {
            PlayerPrefs.SetInt("NoAds", 1);
        }
        else
        {
            ParkourRunner.Scripts.Managers.ProgressManager.AddCoin(100);
        }
    }

    public static void GetBonus(string bonusName)
    {
        PlayerPrefs.SetInt(bonusName, PlayerPrefs.GetInt(bonusName) +1);
    }

}
