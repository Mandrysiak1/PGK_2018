using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddTablesOnTime : MonoBehaviour {

    [SerializeField]
    private float time = 0;
    [SerializeField]
    private bool go;
    [SerializeField]
    private TableScript table1, table2;
    [SerializeField]
    private GameObject UI1, UI2;
    private bool done = false;
    

	void Start () {
        
        table1.enabled = false;
        UI1.active = false;
    }

    void Update () {
        time += Time.deltaTime;
        CheckTime();
    }

    private void CheckTime()
    {
        if (time >= 10 && done == false)
        {
            AddTablesToScene();
            done = true;
        }
    }

    private void AddTablesToScene()
    {
        table1.enabled = true;

        StartCoroutine(wait());
      
            //table1.Start();
            
        
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(0.5f);
        UI1.active = true;
        UI1.GetComponent<RawImage>().enabled = true;
    }
}
