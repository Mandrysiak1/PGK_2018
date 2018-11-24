using Assets;
using Assets.PGKScripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Order
{
    public Guid ID { get; private set; }
    public OrderItem Item { get; private set; }
    public float StartTime { get; private set; }
    public int Size = 1;
    public int FilledSize = 0;

    public Order(OrderItem item, float startTime, int size)
    {
        Item = item;
        StartTime = startTime;
        Size = size;
        FilledSize = 0;
        ID = Guid.NewGuid();
    }

    public override bool Equals(object obj)
    {
        var order = obj as Order;
        return order != null &&
               ID == order.ID;
    }
    public override int GetHashCode()
    {
        var hashCode = 196815894;
        hashCode = hashCode * -1521134295 ^ ID.GetHashCode();
        return hashCode;
    }
}
