using System.Collections.Generic;
using UnityEngine;

public abstract class OrderRequest : MonoBehaviour
{
    public abstract IEnumerable<OrderItem> IntroduceItems { get; }
    public abstract Order MakeOrder();
}
