using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderController : MonoBehaviour {

    private  MainScript mainScript;
    private Scene currentScene;
    public OrderController()
    {

    }

    void Start () {
        mainScript = FindObjectOfType<MainScript>();
        
    }

    void Update()
    {
        CheckNormalTables();
       // CheckAdnitionalTables();

    }

    private void CheckAdnitionalTables()
    {
        throw new NotImplementedException();
    }

    private void CheckNormalTables()
    {
        for (int i = mainScript.AwaitingTables.Count - 1; i >= 0; i--)
        {
            Table y = mainScript.AwaitingTables[i];
            if (y.ControlOrder(mainScript.GameTime, mainScript.moodDecreaseValue))
            {
                y.Mood = 20;
                mainScript.WinStreak += 1;
                mainScript.AwaitingTables.RemoveAt(i);
                mainScript.FreeTables.Add(y);
            }

        }
         
    }

}
