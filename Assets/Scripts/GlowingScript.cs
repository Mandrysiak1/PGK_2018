using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlowingScript : MonoBehaviour {

    private bool hasPlayer = false;
    public GameObject[] objects;
    private Outline[] outlines;
    public Color color;

    void Start () {
        outlines = new Outline[objects.Length];
        for(int i = 0; i < objects.Length; i++)
        {
            outlines[i] = objects[i].GetComponent<Outline>();
            if(objects[i].tag == "Obstacle")
            {
                outlines[i].enabled = true;
                outlines[i].OutlineColor = color;
                outlines[i].OutlineWidth = 1f;
            }
            else if (objects[i].tag == "SignElement")
            {
                outlines[i].enabled = true;
                outlines[i].OutlineColor = color;
                outlines[i].OutlineWidth = 2f;
            }
            else
            {
                outlines[i].enabled = false;
            }
        }
    }

	void Update () {
        Vector3 move;
        if (hasPlayer)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Obstacle")
                {
                    outlines[i].OutlineColor = Color.red;
                    outlines[i].OutlineWidth = 2f;
                    move = new Vector3(0, 0.95f, 0);
                    objects[i].transform.parent.GetComponentInChildren<Image>().transform.rotation = Quaternion.Euler(25, 0, 0);
                    objects[i].transform.parent.GetComponentInChildren<Image>().transform.position = objects[i].transform.position + move;
                    objects[i].transform.parent.GetComponentInChildren<Image>().enabled = true;
                }
                else if (objects[i].tag == "SignElement")
                {
                    outlines[i].OutlineColor = Color.white;
                    outlines[i].OutlineWidth = 2f;
                }
                else
                {
                    outlines[i].enabled = true;
                }
            }
        }
        else if(hasPlayer != true)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Obstacle")
                {
                    outlines[i].OutlineColor = color;
                    outlines[i].OutlineWidth = 1f;
                    objects[i].transform.parent.GetComponentInChildren<Image>().enabled = false;
                }
                else if (objects[i].tag == "SignElement")
                {
                    outlines[i].OutlineColor = color;
                    outlines[i].OutlineWidth = 2f;
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
