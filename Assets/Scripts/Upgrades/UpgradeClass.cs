using Assets.PGKScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public static class UpgradeClass {

    public static int preGameTip;
    public static int Tip=0;
    public static float SpeedModif = 0f;
    public static int BeerModif = 1;
    public static int BeerTimes = 1;
    public static int SpeedTimes = 1;
    public static bool isenabled = false;
    public static bool Vulnerable = true;
    public static bool invulnerabilityPurchased = false;

    public static void ChangeSpeedModif()
    {
        int cost = 5*SpeedTimes;
        if(Tip >= cost)
        {
            Tip -= cost;
            SpeedModif += 0.05f;
            SpeedTimes++;
        }
    }

    public static void ChangeMaxBeer()
    {
        int cost = 10 * BeerTimes;
        if(Tip >= cost)
        {
            Tip -= cost;
            OrderMediator.Instance.MaxBeer += 1;
            BeerTimes++;
        }
    }

    public static void MarioStar()
    {
        int cost = 1000;
        if (Tip >= cost && invulnerabilityPurchased == false)
        {
            Tip -= cost;
            Vulnerable = false;
            invulnerabilityPurchased = true;
        }
    }

}
