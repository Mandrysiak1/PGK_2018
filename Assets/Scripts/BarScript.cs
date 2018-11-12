using Assets.PGKScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    private bool hasPlayer = false;
    private LevelScene LevelScene;


    void Start ()
    {
        LevelScene = FindObjectOfType<LevelScene>();
    }

	void Update ()
    {
        if (Input.GetButtonDown("Submit") && hasPlayer == true)
        {
            LevelScene.Player.AddBeer();
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
