using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingScriptTutorial : MonoBehaviour
{

    public bool hasPlayer = false;
    public GameObject[] objects;
    public Outline[] outlines;
    public Color color;

    void Start()
    {
        outlines = new Outline[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            outlines[i] = objects[i].GetComponent<Outline>();
            if (objects[i].tag == "Obstacle")
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

    void Update()
    {
        if (hasPlayer)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Obstacle")
                {
                    outlines[i].OutlineColor = Color.red;
                    outlines[i].OutlineWidth = 2f;
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
        else if (hasPlayer != true)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i].tag == "Obstacle")
                {
                    outlines[i].OutlineColor = color;
                    outlines[i].OutlineWidth = 1f;
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
