using UnityEngine;

public class IncreaseMoodOnOrderUpdate : MonoBehaviour
{
    public float MoodPerOrder = 1.0f;
    public float CompletionMoodBonus = 0.25f;

    [SerializeField]
    private GameContext Context;

    private void Start()
    {
        GameContext.FindIfNull(ref Context);
        Context.Orders.OrderUpdated.AddListener(OnOrderUpdated);
    }

    private void OnOrderUpdated(OrderSource source, Order order, float previousProgress)
    {
        float progressIncrease = order.Progress - previousProgress;
        source.Mood += MoodPerOrder * progressIncrease;
        if (order.IsFilled)
            source.Mood += CompletionMoodBonus;
    }
}
