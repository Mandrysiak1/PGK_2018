using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public float MinimalDelayBetweenEvents;
    private  List<Table> awaitingTables = new List<Table>();
    private float ChanceOfEvent = 0;

    public delegate void GenerateObstacle();
    public event GenerateObstacle OnGenerateObstacle;


    void Start()
    {
      
        awaitingTables =  GameObject.Find("GameManager").GetComponent<MainScript>().AwaitingTables;

        InvokeRepeating("CalculateChanceOfEvent", 1f, 1f);
    }


    private void CalculateChanceOfEvent()
    {
        ChanceOfEvent = 0;

        foreach (var x in awaitingTables)
        {
            if (x.Mood <= 8 && x.Mood > 4)
            {
                ChanceOfEvent += 25;
            }
            else if (x.Mood <= 4 && x.Mood >= 0)
            {
                ChanceOfEvent += 40;
            }
            else
            {
                ; //DO NOTHING
            }      
        }
        CheckIfEventHappens();
    }

    private void CheckIfEventHappens()
    {
        if (Random.Range(1,100) <= ChanceOfEvent)
        {
            if (OnGenerateObstacle != null)
            {
                OnGenerateObstacle();
            }

           CancelInvoke("CalculateChanceOfEvent");

           InvokeRepeating("CalculateChanceOfEvent", MinimalDelayBetweenEvents, 1f);
   
        }

    }


}