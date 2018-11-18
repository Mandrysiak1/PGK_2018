using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPlate : MonoBehaviour
{
    /// <summary>
    /// <para>item</para>
    /// <para>current item amount</para>
    /// <para>previous item amount</para>
    /// </summary>
    [Serializable]
    public class ItemAmountChanged : UnityEvent<OrderItem, int, int> { }
    public ItemAmountChanged OnItemAmountChanged;

    /// <summary>
    /// <para>new maximum capacity</para>
    /// <para>old maximum capacity</para>
    /// </summary>
    [Serializable]
    public class MaximumCapacityChanged : UnityEvent<int, int> {}
    public MaximumCapacityChanged OnMaximumCapacityChanged;

    public int MaximumCapacity
    {
        get { return _MaximumCapacity + UpgradeClass.BeerModif; }
        set
        {
            int old = _MaximumCapacity;
            _MaximumCapacity = value;
            if(old != _MaximumCapacity)
                OnMaximumCapacityChanged.Invoke(_MaximumCapacity, old);
        }
    }

    public int CurrentCapacity { get; private set; }

    [SerializeField]
    private int _MaximumCapacity = 5;

    private Dictionary<OrderItem, int> orderItemsOnPlate = new Dictionary<OrderItem, int>();

    public void RemoveItem(OrderItem item, int removeAmount = 1)
    {
        int amount;
        if (orderItemsOnPlate.TryGetValue(item, out amount))
        {
            if (amount > 0)
            {
                int old = amount;
                amount -= removeAmount;
                CurrentCapacity -= removeAmount;

                orderItemsOnPlate[item] = amount;

                OnItemAmountChanged.Invoke(item, amount, old);
            }
        }
    }

    public void AddItem(OrderItem item)
    {
        if(CurrentCapacity < MaximumCapacity)
        {
            int amount;
            if (!orderItemsOnPlate.TryGetValue(item, out amount))
                amount = 0;
            int old = amount;
            ++amount;
            ++CurrentCapacity;
            orderItemsOnPlate[item] = amount;

            OnItemAmountChanged.Invoke(item, amount, old);
        }
    }

    public int GetItemQuantityOnPlate(OrderItem x)
    {
        if (orderItemsOnPlate.ContainsKey(x))
        {
            return orderItemsOnPlate[x];
        }
        else return 0;
    }

    public void RemoveAll()
    {
        // We have to iterate over copy, because modifying the collection during iteration causes
        // InvalidOperationException: out of sync

        var copy = new Dictionary<OrderItem, int>(orderItemsOnPlate);
        foreach (var kv in copy)
        {
            RemoveItem(kv.Key, kv.Value);
        }
    }
}
