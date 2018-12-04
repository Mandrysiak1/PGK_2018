using UnityEngine;

public class Player : MonoBehaviour
{
    public int BeersHandedOut { get; private set; }
    public bool Vulnerable = true;

    public PlayerPlate Plate
    {
        get { return _Plate; }
    }

    [SerializeField]
    private PlayerPlate _Plate;

    void Awake()
    {
        if(_Plate == null)
            Debug.LogWarning("Player has no plate!");
        BeersHandedOut = 0;
        Vulnerable = UpgradeClass.Vulnerable;
    }

    public void ResetBeersHandedOut()
    {
        this.BeersHandedOut = 0;
    }

    public void RemoveBeer(OrderItem x)
    {
        Plate.RemoveItem(x);
        System.Random rnd = new System.Random();
    }

    public void AddOrderItemOnPlate(OrderItem x)
    {
        Plate.AddItem(x);
        Debug.Log("Na tacy jest : " + Plate.GetItemQuantityOnPlate(x) + " " + x.name);
    }

    public int GetItemOrderOnPlateQuantity(OrderItem x)
    {
        return Plate.GetItemQuantityOnPlate(x);
    }

    public void ResetPlate()
    {
        Plate.RemoveAll();
    }
}
