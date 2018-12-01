using UnityEngine;

public class AddWinStreakWhenFilledOrder : MonoBehaviour
{
    public int Amount = 1;

    [SerializeField]
    private OrderController Orders;

    [SerializeField]
    private MonoWinStreakSource WinStreak;

    private void Start()
    {
        Orders.OrderFilled.AddListener(OnOrderFilled);
    }

    private void OnOrderFilled(OrderSource source, Order order)
    {
        WinStreak.WinStreak += Amount;
    }
}
