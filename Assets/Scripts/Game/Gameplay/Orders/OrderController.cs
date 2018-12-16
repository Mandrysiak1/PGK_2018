using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class OrderController : MonoBehaviour
{
    [Serializable]
    public class OrderEvent : UnityEvent<OrderSource, Order> {}
    public class OrderChange : UnityEvent<OrderSource, Order> {}


    public OrderEvent OrderAdded;
    public OrderEvent OrderFilled;
    public OrderChange OrderUpdated = new OrderChange();
    public OrderChange UnableEvent = new OrderChange();

    [SerializeField]
    private PlayerPlate Plate;
    [SerializeField]
    private OrderSourceProvider Sources;

    private MainScript MainScript;

    private Dictionary<OrderSource, Order> SourceToOrder = new Dictionary<OrderSource, Order>();
    private Dictionary<Order, OrderSource> OrderToSource = new Dictionary<Order, OrderSource>();

    public IEnumerable<OrderSource> FreeSources
    {
        get
        {
            return Sources.Sources.Where(source => !HasOrder(source));
        }
    }

    public IEnumerable<OrderSource> AwaitingSources
    {
        get
        {
            return Sources.Sources.Where(HasOrder);
        }
    }

    public IEnumerable<Order> Orders
    {
        get
        {
            return SourceToOrder.Values;
        }
    }

    void Start ()
    {
        MainScript = FindObjectOfType<MainScript>();
    }

    public bool HasOrder(OrderSource source)
    {
        return SourceToOrder.ContainsKey(source);
    }

    public Order GetOrderForSource(OrderSource source)
    {
        Order order;
        if (!SourceToOrder.TryGetValue(source, out order))
            return null;
        return order;
    }

    public void AddOrder(OrderSource source, Order order)
    {
        if (HasOrder(source))
        {
            Debug.LogWarningFormat("{0} OrderSource already has an order!", source.name);
            return;
        }

        source.CurrentOrder = order;
        SourceToOrder[source] = order;
        OrderToSource[order] = source;
        OrderAdded.Invoke(source, order);
        source.Refresh();
    }


    public bool TryUpdateOrderSource(OrderSource source)
    {
        Order order = GetOrderForSource(source);
        if (order != null)
        {
            bool wasUpdated = order.TryFill(new OrderContext
            {
                Plate = Plate
            });

            if (wasUpdated)
            {
                source.Refresh();
                OrderUpdated.Invoke(source, order);
                if (order.IsFilled)
                {
                    FillOrder(order);
                }
                return true;
            }
        }
        UnableEvent.Invoke(source, order);
        return false;
    }

    private void FillOrder(Order order)
    {
        OrderSource source = OrderToSource[order];
        source.Mood = 1.0f;
        RemoveOrder(order);
        source.Refresh();
        OrderFilled.Invoke(source, order);

    }

    private void RemoveOrder(Order order)
    {
        OrderSource source = OrderToSource[order];

        source.CurrentOrder = null;
        SourceToOrder.Remove(source);
        OrderToSource.Remove(order);
        source.Refresh();

    }

    public bool CanFillOrder(OrderSource source)
    {
        Order order = GetOrderForSource(source);
        if (order == null)
            return false;

        return order.CanBeFilled(new OrderContext {Plate = Plate });
    }
}
