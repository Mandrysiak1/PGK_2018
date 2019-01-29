using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PressBToJump : MonoBehaviour {
    private bool hasPlayer = false;
    public Canvas text;
    // Use this for initialization
    void Start () {
        text.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hasPlayer)
        {
            text.enabled = true;
        }
        else if (hasPlayer != true)
        {
            text.enabled = false;
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
