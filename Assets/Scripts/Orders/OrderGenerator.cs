
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class OrderGenerator : MonoBehaviour
{

    
    private List<OrderItem> possibleRequests = new List<OrderItem>();



    private  MainScript mainScript;

    private float nextOrderTime = 0;

    void Start()
    {
        
   
    }

    
    private void CalculateNextOrderTime()
    {
        nextOrderTime = Random.Range(3, 7);
    }

    void Update()
    {
        mainScript = FindObjectOfType<MainScript>();
        ChechIfOrderNeeded();
      
    }

    private void ChechIfOrderNeeded()
    {
        if(mainScript.GetTime() >= mainScript.GetTime() + nextOrderTime)
        {
            GenerateOrder();
        }


    }

    private void GenerateOrder()
    {

        if(mainScript.FreeTables.Count != 0)
        {
            int randomTable = Random.Range(0, mainScript.FreeTables.Count);
            Debug.Log(randomTable);
            Debug.Log(mainScript.FreeTables[0].possibleOrders.Count);
            OrderItem RandomOrder = mainScript.FreeTables[randomTable].possibleOrders[Random.Range(0, mainScript.FreeTables[randomTable].possibleOrders.Count)];


            mainScript.FreeTables[randomTable].CurrOrder = new Order(mainScript.GetTime(), RandomOrder);

            mainScript.AwaitingTables.Add(mainScript.FreeTables[randomTable]);

            mainScript.FreeTables.RemoveAt(randomTable);
        }

        
    }
}
