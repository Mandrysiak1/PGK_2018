using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderController : MonoBehaviour {

    private MainScript MainScript;


    void Start () {

        MainScript = FindObjectOfType<MainScript>();
        
    }

    void Update()
    {
       
        CheckNormalTables();

    }


    private void CheckNormalTables()
    {
        if(MainScript.AwaitingTables.Count != 0)
        {
            for (int i = MainScript.AwaitingTables.Count - 1; i >= 0; i--)
            {
                Table y = MainScript.AwaitingTables[i];
                if (y.ControlOrder(MainScript.GameTime, MainScript.moodDecreaseValue))
                {
                    y.Mood = 20;
                    MainScript.WinStreak += 1;
                    MainScript.AwaitingTables.RemoveAt(i);
                    MainScript.FreeTables.Add(y);
                }

            }

        }
       
         
    }

}
