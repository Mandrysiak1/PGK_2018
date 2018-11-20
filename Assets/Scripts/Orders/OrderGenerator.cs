
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class OrderGenerator : MonoBehaviour
{
    private List<OrderItem> possibleRequests = new List<OrderItem>();

    private  MainScript mainScript;

    public delegate void WitchUI();
    public event WitchUI OnWitchUI;

    private float nextOrderTime = 0;

    void Start()
    {
        mainScript = FindObjectOfType<MainScript>();
        CalculateNextOrderTime();

    }

    
    private void CalculateNextOrderTime()
    {
        nextOrderTime  = mainScript.GetTime() +  Random.Range(4, 10);
    }

    void Update()
    {

        ChechIfOrderNeeded();
      
    }

    private void ChechIfOrderNeeded()
    {
        if(mainScript.GetTime() >=  nextOrderTime)
        {
            CalculateNextOrderTime();
            GenerateOrder();
        }


    }

    private void GenerateOrder()
    {
        
        if (mainScript.FreeTables.Count != 0)
        {
            int randomTable = Random.Range(0, mainScript.FreeTables.Count);

            OrderItem RandomOrder = mainScript.FreeTables[randomTable].possibleOrders[Random.Range(0, mainScript.FreeTables[randomTable].possibleOrders.Count)];
            
            if(RandomOrder.name == "WitchPotion")
            {
                if(Random.Range(0,100) < 80)
                {
                    randomTable = Random.Range(0, mainScript.FreeTables.Count);

                    RandomOrder = mainScript.FreeTables[randomTable].possibleOrders[Random.Range(0, mainScript.FreeTables[randomTable].possibleOrders.Count)];

                }
            }
            if (RandomOrder.name == "WitchPotion")
            {
                OnWitchUI();
            }

            mainScript.FreeTables[randomTable].CurrOrder = new Order(mainScript.GetTime(), RandomOrder);

            mainScript.AwaitingTables.Add(mainScript.FreeTables[randomTable]);

            mainScript.FreeTables.RemoveAt(randomTable);

            Debug.Log("Wygenerowano zamówienie :" + RandomOrder.name);
        }

        
    }
}
