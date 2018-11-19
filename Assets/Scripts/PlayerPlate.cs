using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

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

    internal void RemoveRandomItem()
    {
        if (orderItemsOnPlate.Count != 0)
        {
            var values = orderItemsOnPlate.Values.ToList();
            bool PlateIsEmpty = true;
            foreach (var x in values)
            {
                if (x != 0)
                {
                    PlateIsEmpty = false;
                }
            }


            if (PlateIsEmpty != true)
            {
                var Keys = orderItemsOnPlate.Keys.ToList();
                OrderItem RandomItem;
                do
                {
                    RandomItem = Keys[UnityEngine.Random.Range(0, Keys.Count)];

                } while (orderItemsOnPlate[RandomItem] == 0);


                RemoveItem(RandomItem);
            }
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
}
