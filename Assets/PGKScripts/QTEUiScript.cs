using System;
using System.Collections;
using Assets.PGKScripts.Interfaces;
using UnityEngine;

public class QTEUiScript : MonoBehaviour, IQteUI {

 
    public Canvas q;
    public Canvas e;
    public Canvas x;
    public Canvas c;
    public Canvas qteBackground;
    public Canvas success;
    public Canvas failure;

    private IQteScript[] qteScripts;

    // Use this for initialization
    void Start () {
        success.enabled = false;
        failure.enabled = false;
        q.enabled = false;
        e.enabled = false;
        x.enabled = false;
        c.enabled = false;
        qteBackground.enabled = false;
        qteScripts = (QTEScript[])FindObjectsOfType(typeof(QTEScript));
           foreach(var s in qteScripts)
            s.PropertyChanged += QteScript_PropertyChanged;
    }

    private void QteScript_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var qteScript = (QTEScript)sender;
        if(e.PropertyName.Equals("CurrentChar"))
            this.SetImage(qteScript.CurrentChar);
        if(e.PropertyName.Equals("Success"))
        {
            if (qteScript.Success)
            {
                success.enabled = true;
                StartCoroutine(Wait("success"));
            }
            else
                success.enabled = false;
        }
        if (e.PropertyName.Equals("Failure"))
        {
            if (qteScript.Failure)
            {
                failure.enabled = true;
                StartCoroutine(Wait("failure"));
            }
            else
                failure.enabled = false;
        }
    }

    IEnumerator Wait(string type)
    {
        yield return new WaitForSeconds(1);
        if (type == "success")
            success.enabled = false;
        else if (type == "failure")
            failure.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
	}

    public void SetImage(string image)
    {
        if (image == "q")
        {
            qteBackground.enabled = true;
            q.enabled = true;
            e.enabled = false;
            x.enabled = false;
            c.enabled = false;
        }
        else if (image == "e")
        {
            qteBackground.enabled = true;
            q.enabled = false;
            e.enabled = true;
            x.enabled = false;
            c.enabled = false;
        }
        else if (image == "x")
        {
            qteBackground.enabled = true;
            q.enabled = false;
            e.enabled = false;
            x.enabled = true;
            c.enabled = false;
        }
        else if (image == "c")
        {
            qteBackground.enabled = true;
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
            qteBackground.enabled = false;
        }

    }

}
