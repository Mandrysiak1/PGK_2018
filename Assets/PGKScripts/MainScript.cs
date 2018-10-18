using Assets;
using Assets.PGKScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainScript : MonoBehaviour
{
    public static Player player = new Player();
    private float time = 0f;
    private float nextOrderTime = 0f;
    private float orderDeadline = 10f;
    System.Random randomNum = new System.Random();

    List<Table> awaitingTables = new List<Table>();
    List<Table> freeTables = new List<Table>();

    public MainScript()
    {
    }

    public void AddFreeTable(object table)
    {
        Debug.Log("Added table " + ((Table)table).ID);
        freeTables.Add((Table)table);
    }

    public Player GetPlayer()
    {
        return player;
    }


    void Update()
    {
        Debug.Log("assda");
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
        int offset = randomNum.Next(7, 9);
        nextOrderTime = time + offset;
        //TODO:Wymyśleć jakiś sposób na zmiane czasu;
    }

    public void GenerateOrder()
    {

        if (freeTables.Count != 0)
        {

            
            int randomTable = randomNum.Next(0, freeTables.Count);
            int sizeOfOrder = randomNum.Next(1, 4);
            Debug.Log("  #SYSINFO: Wygenerowano zamówienie o rozmiarze: " + sizeOfOrder);
            freeTables[randomTable].CurrOrder = new Order(time, time + orderDeadline, sizeOfOrder);

            awaitingTables.Add(freeTables[randomTable]);

            freeTables.Remove(freeTables[randomTable]);

         }else
        {
            Debug.Log("  #SYSINFO: Nie wygenerowano zamówienia,wszystkie stoliki zajęte!");
        }
    }


    public void ControlOrders()
    {
        for (int i = 0; i < awaitingTables.Count; i++)
        {
            Table x = awaitingTables[i];
            x.ControlOrder(time);
            if(x.shouldBeFree == true)
            {
                Debug.Log("  #SYSINFO: Zwolniono stolik");
                awaitingTables.Remove(x);
                freeTables.Add(x);
                x.shouldBeFree = false;
                
            }
        }
    }
}

