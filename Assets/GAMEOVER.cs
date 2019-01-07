using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMEOVER : MonoBehaviour {

    private MainScript main;
	// Use this for initialization
	void Start () {
        main = FindObjectOfType<MainScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("asd fpd;sgn fkpadfngd");
            main.GameOver();
        }
    }
}
