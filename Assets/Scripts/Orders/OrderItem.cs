using UnityEngine;

[CreateAssetMenu(menuName = "Beerfest/OrderItem")]

public class OrderItem : ScriptableObject
{

    public OrderMediator mediator = OrderMediator.Instance;

    public string Name;

    public int MaximumOrderSize {
        get
        {
            return mediator.MaximumOrderSize(this);
        }
    }

    public Sprite Sprite;

}
