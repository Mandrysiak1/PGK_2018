using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddSantaBonus : MonoBehaviour {

    [SerializeField]
    private OrderController OrderController;

    [SerializeField]
    private OrderSource OrderSource;

    [SerializeField]
    private List<OrderItem> OrderItems;
 
	void Start () {

        OrderController.OrderFilled.AddListener(OnOrderFilled);
        
	}

    private void OnOrderFilled(OrderSource source, Order order)
    {
        
        if(CheckIfSantaRequest(order.Item))
        {

        }

    }

    private bool CheckIfSantaRequest(OrderItem item)
    {
        if (!OrderItems.Contains(item))
        {
            return false;
        }else
        {
            return true;
        }
        
    }

}
