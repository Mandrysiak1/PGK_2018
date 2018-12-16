using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class AddSantaBonus : MonoBehaviour
{
    public UnityEvent OnSantaInfo;

    [SerializeField]
    private List<OrderSource> orderSources;
    [SerializeField]
    private OrderItem SantaOrder;

    private GameContext Context;
    private HashSet<OrderSource> DisabledSources = new HashSet<OrderSource>();

    void Start ()
    {
        GameContext.FindIfNull(ref Context);
        Context.Orders.OrderFilled.AddListener(OnOrderFilled);
    }

    private void OnOrderFilled(OrderSource source, Order order)
    {
        if(source == transform.GetComponent<OrderSource>())
        {
            bool allSourcesAreAlreadyDisabled = orderSources.All(IsAlreadyDisabled);

            if (!allSourcesAreAlreadyDisabled)
            {
                DisableRandomTable();
                OnSantaInfo.Invoke();
            }
        }

    }

    private bool IsAlreadyDisabled(OrderSource source)
    {
        return DisabledSources.Contains(source);
    }

    private void DisableRandomTable()
    {
        OrderSource source = orderSources.Except(DisabledSources).ToList().Random();
        source.CurrentOrder = new ItemOrderRequest.BasicOrder(SantaOrder, 1);
        source.Refresh();
        source.enabled = false;
        DisabledSources.Add(source);
    }

}
