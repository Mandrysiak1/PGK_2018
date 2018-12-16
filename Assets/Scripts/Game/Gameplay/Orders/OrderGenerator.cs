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
            if (source.enabled && source.CanIssueOrder(Controller))
            {
                GenerateOrder(source);
                Debug.LogFormat("New order for {0}!", source.name);
            }
        }
    }

    private void GenerateOrder(OrderSource source)
    {
        OrderRequest request = source.PossibleRequests.Random();
        Order order = request.MakeOrder();
        Controller.AddOrder(source, order);
    }
}
