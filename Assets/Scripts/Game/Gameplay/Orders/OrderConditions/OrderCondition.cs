using UnityEngine;

public abstract class OrderCondition : MonoBehaviour
{
    public abstract bool CanIssueOrder(OrderSource source, OrderController controller);
    public abstract void Tick(Order currentOrder, float deltaTime);
}
