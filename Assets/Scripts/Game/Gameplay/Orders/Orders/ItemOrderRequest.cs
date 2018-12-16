
using System.Collections.Generic;
using UnityEngine;

public class ItemOrderRequest : OrderRequest
{
    [Range(0, 50)]
    public int MinimumAmount = 0;
    [Range(1, 50)]
    public int MaximumAmount = 1;

    public OrderItem Item;

    public override Order MakeOrder()
    {
        int amount = Random.Range(MinimumAmount, MaximumAmount + 1);

        return new BasicOrder(Item, amount);
    }

    public class BasicOrder : Order
    {
        public override float FillRate
        {
            get { return (float)FilledAmount / Amount; }
        }

        public override bool IsFilled
        {
            get { return Amount == FilledAmount; }
        }

        protected OrderItem Item;
        protected int Amount;
        protected int FilledAmount;

        public BasicOrder(OrderItem item, int amount)
        {
            Item = item;
            Amount = amount;
            FilledAmount = 0;
        }

        public override bool CanBeFilled(OrderContext context)
        {
            if (IsFilled)
                return false;

            int amountOnPlate = context.Plate.GetItemQuantityOnPlate(Item);
            return amountOnPlate > 0;
        }

        public override bool TryFill(OrderContext context)
        {
            if (!CanBeFilled(context))
                return false;

            FilledAmount++;
            context.Plate.RemoveItem(Item);
            return true;
        }

        public override OrderVisualization Visualize()
        {
            return new OrderVisualization
            {
                Items = new Dictionary<OrderItem, int>{{Item, Amount - FilledAmount}}
            };
        }
    }
}
