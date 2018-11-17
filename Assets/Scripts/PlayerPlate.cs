using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPlate : MonoBehaviour
{
    /// <summary>
    /// <para>current beer amount</para>
    /// <para>previous beer amount</para>
    /// </summary>
    [Serializable]
    public class BeerCountEvent : UnityEvent<OrderItem, int, int> { }
    public BeerCountEvent OnBeerCountChanged;
    private Dictionary<OrderItem, int> orderItemsOnPlate = new Dictionary<OrderItem, int>();


    public int StartingBeers = 0;

    public void RemoveItem(OrderItem x)
    {
        if (orderItemsOnPlate.ContainsKey(x))
        {
            int old = orderItemsOnPlate[x];
            if (orderItemsOnPlate[x] > 0)
            {
                orderItemsOnPlate[x] -= 1;
                OnBeerCountChanged.Invoke(x, orderItemsOnPlate[x], old);
            }

        }
    }

    public void AddItem(OrderItem x)
    {
        if (orderItemsOnPlate.ContainsKey(x) == true)
        {
            int old = orderItemsOnPlate[x];
            if (orderItemsOnPlate[x] < x.MaximumOrderSize)
                orderItemsOnPlate[x] += 1;
            OnBeerCountChanged.Invoke(x, orderItemsOnPlate[x], old);
        }
        else
        {
            int old = 0;
            orderItemsOnPlate.Add(x, 1);
            OnBeerCountChanged.Invoke(x, orderItemsOnPlate[x], old);
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
