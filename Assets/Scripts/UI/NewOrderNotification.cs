using System.Linq;
using UnityEngine;

public class NewOrderNotification : Notification
{
    public string Format = "{0} has made an order!";
    [SerializeField]
    private OrderSource[] Sources;

    private GameContext Context;

    protected new void Start()
    {
        base.Start();
        GameContext.FindIfNull(ref Context);

        if (Context == null)
            return;

        Context.Orders.OrderAdded.AddListener(NewOrder);
    }

    private void NewOrder(OrderSource source, Order order)
    {
        if(Sources.Contains(source))
            Show(string.Format(Format, source.name, "", ""));
    }
}
