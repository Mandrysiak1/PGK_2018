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


    public delegate void SantaInfo();
    public event SantaInfo OnSantaInfo;


    private OrderSource x;
    void Start () {

        GameContext.FindIfNull(ref Context);

        Context.Orders.OrderFilled.AddListener(OnOrderFilled);
    }

    private void OnOrderFilled(OrderSource source, Order order)
    {
        
        if(source == transform.GetComponent<OrderSource>())
        {

            bool end  = false;
            while(!end)
            {
                OrderSource newOne = orderSources.Random();
                if(newOne != x)
                {
                    x = newOne;
                    end = true;
                }
               
            }

            x = orderSources.Random();
            if (OnSantaInfo != null)
            {
                OnSantaInfo();
            }
        }

    }

    private void Update()
    {
        DiseableTable();
    }

    private void DiseableTable()
    {
    
        if(x != null)
        {
            x.Mood = 1.0f;
            x.MoodDecreaseRate = 0.0f;
            x.CurrentOrder = new Order(SantaOrder, 0.0f, 100);


        }


    }
}
