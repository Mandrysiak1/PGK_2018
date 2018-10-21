using Assets;
using Assets.PGKScripts;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainScript : MonoBehaviour
{
    public static Player player = new Player();
    public Text HowManyBeers;
    private float time = 0f;
    private float nextOrderTime = 0f;
    private float orderDeadline = 9999f; //IMHO niepotrzebne do niczego
    System.Random randomNum = new System.Random();
    public Slider BigBar;
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
        time += Time.deltaTime;
        ControlOrders();
       
        if (time >= nextOrderTime)
        {
            GenerateOrder();
            CalculateNextOrderTime();
        }
        FillTheBAR();
        BeerCountDisplay();
    }

    void CalculateNextOrderTime()
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
        }
        else
        {
            Debug.Log("  #SYSINFO: Nie wygenerowano zamówienia,wszystkie stoliki zajęte!");
        }
    }


    public void ControlOrders()
    {
        for (int i = awaitingTables.Count - 1; i >= 0; i--)
        {
            Table x = awaitingTables[i];
            var shouldBeFree = x.ControlOrder(time);
            if (shouldBeFree == true)
            {
                x.Mood = 20;
                Debug.Log("  #SYSINFO: Zwolniono stolik");
                awaitingTables.RemoveAt(i);
                freeTables.Add(x);
            }
        }
    }

    private void FillTheBAR()
    {
        int i = 0;
        int threshold = 4;
        foreach(Table t in awaitingTables)
        {
            if (t.Mood < threshold) i++;
        }
        BigBar.value += Time.deltaTime * i;
    }

    private void BeerCountDisplay()
    {
        HowManyBeers.text = "Trzymasz " + player.getBOP() + " piw.";
    }

}

