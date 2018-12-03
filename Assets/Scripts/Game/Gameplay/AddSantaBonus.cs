using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSantaBonus : MonoBehaviour {

    [SerializeField]
    private OrderSource OrderSource;
    [SerializeField]
    private List<OrderItem> OrderItems;

    private GameContext Context;


 
	void Start () {

        GameContext.FindIfNull(ref Context);

        Context.Orders.OrderFilled.AddListener(OnOrderFilled);
    }

    private void OnOrderFilled(OrderSource source, Order order)
    {
        
        if(source == transform.GetComponent<OrderSource>())
        {

        }

    }


}
