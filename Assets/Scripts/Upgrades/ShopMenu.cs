using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    private GameFlowController Flow;

    public ShopProductItemUI PlateCapacity, Speed, Invulnerability;

    public void Quit()
    {
        Flow.StartNextLevel();
    }

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        int tip = UpgradeClass.Tip;
        int capacityCost = 10 * UpgradeClass.BeerTimes;
        PlateCapacity.Available = tip >= capacityCost;
        PlateCapacity.BuyAction = Buy(UpgradeClass.ChangeMaxBeer);
        PlateCapacity.SetValues(5 + UpgradeClass.BeerModif, capacityCost, 1);

        int speedCost = 5 * UpgradeClass.SpeedTimes;
        Speed.Available = tip >= speedCost;
        Speed.BuyAction = Buy(UpgradeClass.ChangeSpeedModif);
        Speed.SetValues(UpgradeClass.SpeedTimes, speedCost, 1);

        int invulnerabilityCost = 1000;
        Invulnerability.Available = tip >= invulnerabilityCost;
        Invulnerability.Inactive = !UpgradeClass.invulnerabilityPurchased;
        Invulnerability.BuyAction = Buy(UpgradeClass.MarioStar);
        Invulnerability.SetValues(0, invulnerabilityCost, 0);
    }

    private Action Buy(Action performUpgrade)
    {
        return () =>
        {
            performUpgrade();
            Refresh();
        };
    }
}
