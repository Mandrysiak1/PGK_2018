using UnityEngine;

public class OrderMediator
{
    private static OrderMediator lazyInstance;
    public static OrderMediator Instance
    {
        get
        {
            if (lazyInstance == null)
                lazyInstance = new OrderMediator();
            return lazyInstance;
        }
    }
    private OrderMediator()
    {
        MaxBeer = 5 + UpgradeClass.BeerModif;
        MaxWitchPotion = 1;
    }
    public int MaxBeer { get; set; }
    public int MaxWitchPotion { get; set; }
    public int MaximumOrderSize(OrderItem item)
    {
        if (item.Name.Equals("Beer"))
            return MaxBeer;
        if (item.Name.Equals("WitchPotion"))
            return MaxWitchPotion;
        return 0;
    }
}