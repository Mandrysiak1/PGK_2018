using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingScript : MonoBehaviour {

    private bool hasPlayer = false;
    public GameObject table;
    Outline y;
    // Use this for initialization
    void Start () {

        // y = x.GetComponent<MeshRenderer>();
        //y = transform.GetComponent<Outline>();
        //y = GameObject.Find("ThatTable (6)/default").GetComponent<Outline>();
        y = table.GetComponent<Outline>();
    }
	
	// Update is called once per frame
	void Update () {
       
		if(hasPlayer)
        {
            y.enabled = true;
        }else if(hasPlayer != true)
        {
            y.enabled = false;
        }

	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jesteś przy stoliku");
            hasPlayer = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Opuszczasz stolik");
            hasPlayer = false;
        }
    }
}
