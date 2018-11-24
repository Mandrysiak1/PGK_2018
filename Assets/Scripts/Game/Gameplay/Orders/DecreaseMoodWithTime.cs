using UnityEngine;

public class DecreaseMoodWithTime : MonoBehaviour
{
    [SerializeField]
    private OrderController Orders;

    public float RateMultiplier = 1.0f;

    private void Update()
    {
        foreach (OrderSource source in Orders.AwaitingSources)
        {
            float baseChange = source.MoodDecreaseRate * Time.deltaTime;
            float change = baseChange * RateMultiplier;
            source.Mood -= change;
        }
    }
}
