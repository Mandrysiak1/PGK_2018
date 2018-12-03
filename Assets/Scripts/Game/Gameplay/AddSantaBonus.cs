using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AddSantaBonus : MonoBehaviour {

    private GameContext Context;

    [SerializeField]
    private List<OrderSource> orderSources;
    [SerializeField]
    private OrderItem SantaOrder;

    void Start () {

        GameContext.FindIfNull(ref Context);

        Context.Orders.OrderFilled.AddListener(OnOrderFilled);
    }

    private void OnOrderFilled(OrderSource source, Order order)
    {
        
        if(source == transform.GetComponent<OrderSource>())
        {
            DiseableTable();
        }

    }

    private void DiseableTable()
    {

        OrderSource order = orderSources.Random();
    
        order.Mood = 1.0f;
        order.MoodDecreaseRate = 0.0f;
        order.CurrentOrder = new Order(SantaOrder, 0.0f, 0);
        order.IsActive = false;
    }
}
