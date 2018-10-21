using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QTE_UI_Script : MonoBehaviour {

 

    public Canvas q;
    public Canvas e;
    public Canvas x;
    public Canvas c;

    // Use this for initialization
    void Start () {
        q.enabled = false;
        e.enabled = false;
        x.enabled = false;
        c.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        
	}

    public void setImage(string image)
    {
        if (image == "q")
        {
            q.enabled=true;
            e.enabled = false;
            x.enabled = false;
            c.enabled = false;
        }
        else if (image == "e")
        {
            q.enabled = false;
            e.enabled = true;
            x.enabled = false;
            c.enabled = false;
        }
        else if (image == "x")
        {
            q.enabled = false;
            e.enabled = false;
            x.enabled = true;
            c.enabled = false;
        }
        else if (image == "c")
        {
            q.enabled = false;
            e.enabled = false;
            x.enabled = false;
            c.enabled = true;
        }
        else if(image =="0")
        {
            q.enabled = false;
            e.enabled = false;
            x.enabled = false;
            c.enabled = false;
        }

    }

}
