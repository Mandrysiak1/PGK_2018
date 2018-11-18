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

    private Dictionary<OrderItem, int> orderItemsOnPlate = new Dictionary<OrderItem, int>();

    public void RemoveItem(OrderItem item)
    {
        int amount;
        if (orderItemsOnPlate.TryGetValue(item, out amount))
        {
            if (amount > 0)
            {
                int old = amount;
                --amount;
                orderItemsOnPlate[item] = amount;

                OnItemAmountChanged.Invoke(item, amount, old);
            }
        }
    }

    private int GetMaximumItemAmount(OrderItem item)
    {
        return item.MaximumOrderSize;
    }

    public void AddItem(OrderItem item)
    {
        int amount;
        if (!orderItemsOnPlate.TryGetValue(item, out amount))
            amount = 0;
        if(amount < GetMaximumItemAmount(item))
        {
            int old = amount;
            ++amount;
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

    public int Beers
    {
        get { return _Beers; }
        set
        {
            int old = _Beers;
            if (value != old)
            {
                _Beers = value;
                // OnBeerCountChanged.Invoke(_Beers, old);
            }
        }
    }
    private int _Beers;

}
