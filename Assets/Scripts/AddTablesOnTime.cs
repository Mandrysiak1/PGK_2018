using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTablesOnTime : MonoBehaviour {

    [SerializeField]
    private float time = 0, activationTime;
    [SerializeField]
    private TableScript table1, table2;
    [SerializeField]
    private GameObject UI1, UI2;
    [SerializeField]
    private GameObject Guest11, Guest12, Guest21, Guest22;

    private bool done = false;





    void Start () {
        
        table1.enabled = false;
        table2.enabled = false;
        UI1.SetActive(false);
        UI2.SetActive(false);
    }

    void Update () {
        time += Time.deltaTime;
        CheckTime();
    }

    private void CheckTime()
    {
        if (time >= activationTime && done == false)
        {
            AddTablesToScene();
            done = true;
        }
    }

    private void AddTablesToScene()
    {

        var x = FindObjectOfType<LevelScene>().Main.Guests = true;

        Guest11.SetActive(true);
        Guest12.SetActive(true);
        Guest21.SetActive(true);
        Guest22.SetActive(true);
        table1.enabled = true;
        table2.enabled = true;
        UI1.SetActive(true);
        UI2.SetActive(true);

    }
    
}
