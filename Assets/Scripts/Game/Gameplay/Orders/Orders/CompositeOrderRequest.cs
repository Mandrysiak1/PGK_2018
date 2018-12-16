using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompositeOrderRequest : OrderRequest
{
    [SerializeField]
    private List<OrderRequest> Requests;

    public override Order MakeOrder()
    {
        return new CompositeOrder(Requests.Select(request => request.MakeOrder()));
    }

    public class CompositeOrder : Order
    {
        private List<Order> Orders;

        public CompositeOrder(IEnumerable<Order> orders)
        {
            Orders = orders.ToList();
        }

        public override float Progress
        {
            get { return Orders.Average(order => order.Progress); }
        }

        public override bool IsFilled
        {
            get { return Orders.All(order => order.IsFilled); }
        }

        public override bool CanBeFilled(OrderContext context)
        {
            return Orders.Any(order => order.CanBeFilled(context));
        }

        public override bool TryFill(OrderContext context)
        {
            foreach (Order order in Orders)
            {
                if (order.CanBeFilled(context))
                {
                    bool wasUpdated = order.TryFill(context);
                    if (!wasUpdated)
                    {
                        Debug.LogErrorFormat("SOMETHING IS WRONG!!! Order of type {0} claimed that " +
                                             "it can be filled, but trying to fill failed!" +
                                             "FIX IT YOU LAZY SCUM!!!", order.GetType().Name);
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public override OrderVisualization Visualize()
        {
            Dictionary<OrderItem, int> items = new Dictionary<OrderItem, int>();
            foreach (Order order in Orders)
            {
                foreach (var kv in order.Visualize().Items)
                {
                    int amount = kv.Value;
                    if (amount == 0)
                        continue;
                    if (items.ContainsKey(kv.Key))
                        items[kv.Key] += amount;
                    else
                        items[kv.Key] = amount;
                }
            }

            return new OrderVisualization { Items = items };
        }
    }
}
