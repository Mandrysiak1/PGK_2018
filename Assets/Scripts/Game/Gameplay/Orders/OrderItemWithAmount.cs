public class OrderItemWithAmount
{
    public OrderItem Item;
    public int Amount = 0;

    public OrderItemWithAmount(OrderItem item, int amount)
    {
        Item = item;
        Amount = amount;
    }
}
