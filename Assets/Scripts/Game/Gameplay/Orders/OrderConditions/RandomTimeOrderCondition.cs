using UnityEngine;

public class RandomTimeOrderCondition : OrderCondition
{
    [Range(0, 50)]
    public float Minimum = 3;
    [Range(0, 50)]
    public float Maximum = 10;

    private float IdleTime = 0.0f;
    private float? WaitTime = null;

    public override bool CanIssueOrder(OrderSource source, OrderController controller)
    {
        if (!WaitTime.HasValue)
            WaitTime = Random.Range(Minimum, Maximum + 1);

        bool timePassed = IdleTime > WaitTime;
        if (timePassed)
        {
            IdleTime = 0.0f;
            WaitTime = null;
        }
        return timePassed;
    }

    public override void Tick(Order currentOrder, float deltaTime)
    {
        if (currentOrder == null)
            IdleTime += deltaTime;
        else
            IdleTime = 0.0f;
    }
}
