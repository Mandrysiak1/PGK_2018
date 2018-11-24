using System;
using UnityEngine;
using UnityEngine.Events;

public class OrderGenerator : MonoBehaviour
{
    [Serializable]
    public class OrderEvent : UnityEvent<OrderSource, Order>{}

    [SerializeField]
    private OrderController Controller;

    private void Update()
    {
        foreach (OrderSource source in Controller.FreeSources)
        {
            if (source.CanIssueOrder(Controller))
            {
                GenerateOrder(source);
                Debug.LogFormat("New order for {0}!", source.name);
            }
        }
    }

    private void GenerateOrder(OrderSource source)
    {
        OrderItem item = source.PossibleRequests.Random();
        int size = UnityEngine.Random.Range(1, item.MaximumOrderSize + 1);
        Order order = new Order(item, Time.time, size);
        Controller.AddOrder(source, order);
    }
}
