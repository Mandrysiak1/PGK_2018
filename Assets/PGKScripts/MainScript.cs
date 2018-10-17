using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScript : MonoBehaviour
{

    private float time = 0f;
    private float nextOrderTime = 0f;
    private float orderDeadline = 10f;
    System.Random randomNum = new System.Random();

    List<Table> awaitingTables = new List<Table>();
    List<Table> freeTables = new List<Table>();


    private Table table1 = new Table();
    private Table table2 = new Table();
    private Table table3 = new Table();

    public MainScript()
    {
    }

    public void AddFreeTable(object table)
    {
        Debug.Log("Added table " + ((Table)table).ID);
        freeTables.Add((Table)table);
    }

    // Use this for initialization
    void Start()
    { 

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        ControlOrders();
       
        if (time >= nextOrderTime)
        {
            GenerateOrder();
            calculateNextOrderTime();
 

        }

    }

    void calculateNextOrderTime()
    {
        int offset = randomNum.Next(3, 6);
        nextOrderTime = time + offset;
        //TODO:Wymyśleć jakiś sposób na zmiane czasu;
    }

    public void GenerateOrder()
    {

        if (freeTables.Count != 0)
        {

            Debug.Log("Wygenerowano zamówienie");
            int randomTable = randomNum.Next(0, freeTables.Count);
            int sizeOfOrder = randomNum.Next(1, 4);

            freeTables[randomTable].CurrOrder = new Order(time, time + orderDeadline, sizeOfOrder);

            awaitingTables.Add(freeTables[randomTable]);

            freeTables.Remove(freeTables[randomTable]);



        }
    }

    public void ControlOrders()
    {   
        foreach(Table x in awaitingTables)
        {
            x.ControlOrder(time);
        }
    }
}

