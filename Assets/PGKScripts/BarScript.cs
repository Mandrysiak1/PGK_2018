using Assets.PGKScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour {

    private Player myPlayer;
    private bool hasPlayer = false;


    void Start () {
        var x = FindObjectOfType(typeof(MainScript));
        myPlayer = ((MainScript)x).GetPlayer();
       
    }
	void Update () {
        if (Input.GetKeyDown(KeyCode.E) && hasPlayer == true)
        {
            myPlayer.AddBeer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hasPlayer = false;
        }
    }
}
