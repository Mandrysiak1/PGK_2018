using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingScript : MonoBehaviour {

    private bool hasPlayer = false;
    public GameObject[] objects;
    private Outline[] outlines;
    // Use this for initialization
    void Start () {
        outlines = new Outline[objects.Length];
        Debug.Log(objects.Length);
        for(int i = 0; i < objects.Length; i++)
        {
            outlines[i] = objects[i].GetComponent<Outline>();
            if(objects[i].tag == "Obstacle")
            {
                outlines[i].OutlineColor = Color.black;
                outlines[i].OutlineWidth = 1f;
            }
            else
            {
                outlines[i].enabled = false;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(hasPlayer)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Obstacle")
                {
                    outlines[i].OutlineColor = Color.red;
                }
                else
                {
                    outlines[i].enabled = true;
                }
            }
        }else if(hasPlayer != true)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Obstacle")
                {
                    outlines[i].OutlineColor = Color.black;
                    outlines[i].OutlineWidth = 1f;
                }
                else
                {
                    outlines[i].enabled = false;
                }
            }
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
