using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public ShopProductItemUI PlateCapacity, Speed, Invulnerability;

    private void Start()
    {
        Refresh();
    }

    private void Refresh()
    {
        int tip = UpgradeClass.Tip;
        int capacityCost = 10 * UpgradeClass.BeerTimes;
        PlateCapacity.Available = tip > capacityCost;
        PlateCapacity.BuyAction = UpgradeClass.ChangeMaxBeer;

        int speedCost = 5 * UpgradeClass.SpeedTimes;
        Speed.Available = tip > speedCost;
        Speed.BuyAction = UpgradeClass.ChangeSpeedModif;

        int invulnerabilityCost = 1000;
        Invulnerability.Available = tip > invulnerabilityCost;
        Invulnerability.Inactive = !UpgradeClass.invulnerabilityPurchased;
        Invulnerability.BuyAction = UpgradeClass.MarioStar;
    }
}
