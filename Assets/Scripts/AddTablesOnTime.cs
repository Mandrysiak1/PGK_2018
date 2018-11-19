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

    private bool done = false;
    

	void Start () {
        
        table1.enabled = false;
        table2.enabled = false;
        UI1.active = false;
        UI2.active = false;
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

        table1.enabled = true;
        table2.enabled = true;
        UI1.active = true;
        UI2.active = true;

    }
    
}
