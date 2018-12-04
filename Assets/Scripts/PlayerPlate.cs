using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = System.Random;

public class PlayerPlate : MonoBehaviour
{
    public int maximumCapacityMultiplier = 1;
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
    public class MaximumCapacityChanged : UnityEvent<int, int> { }
    public MaximumCapacityChanged OnMaximumCapacityChanged;

    public int MaximumCapacity
    {
        get { return maximumCapacityMultiplier * (_MaximumCapacity + UpgradeClass.BeerModif); }
        set
        {
            int old = _MaximumCapacity;
            _MaximumCapacity = value;
            if (old != _MaximumCapacity)
                OnMaximumCapacityChanged.Invoke(_MaximumCapacity, old);
        }
    }


    public int CurrentCapacity { get; private set; }

    public int UniqueItemCount
    {
        get { return orderItemsOnPlate.Count; }
    }

    public bool Empty
    {
        get { return CurrentCapacity == 0; }
    }

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
                removeAmount = Math.Min(removeAmount, amount);
                int old = amount;
                amount -= removeAmount;
                CurrentCapacity -= removeAmount;

                if (amount >= 0)
                {
                    orderItemsOnPlate[item] = amount;
                }
                else
                {
                    orderItemsOnPlate.Remove(item);
                }

                OnItemAmountChanged.Invoke(item, amount, old);
            }
        }

    }

    public void AddItem(OrderItem item)
    {
        if (CurrentCapacity < MaximumCapacity)
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
        if (x != null) // Quick fix for ArgumentNullException;
        {
            if (orderItemsOnPlate.ContainsKey(x))
            {
                return orderItemsOnPlate[x];
            }
            else return 0;

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

    public void RemoveRandomItem(Random random = null)
    {
        if (!Empty)
        {
            OrderItem randomItem = Items.ToList().Random(random);
            RemoveItem(randomItem);
        }
    }

    public IEnumerable<OrderItem> Items
    {
        get
        {
            var copy = new Dictionary<OrderItem, int>(orderItemsOnPlate);
            foreach (var kv in copy)
            {
                for (int i = 0; i < kv.Value; i++)
                    yield return kv.Key;
            }
        }
    }

    public int getItemCount()
    {
        int i = 0;
        foreach(var item in orderItemsOnPlate)
        {
            i += item.Value;
        }
        return i;
    }
}
