using UnityEngine;

public class SetFullMoodOnOrderCompletion : MonoBehaviour
{
    [SerializeField]
    private GameContext Context;

    private void Start()
    {
        GameContext.FindIfNull(ref Context);
        Context.Orders.OrderFilled.AddListener(OnOrderFilled);
    }

    private void OnOrderFilled(OrderSource source, Order order)
    {
        source.Mood = 1.0f;
    }
}
