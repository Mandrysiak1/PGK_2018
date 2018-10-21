using Assets.PGKScripts.Interfaces;
using UnityEngine;

public class QTEUiScript : MonoBehaviour, IQteUI {

 
    public Canvas q;
    public Canvas e;
    public Canvas x;
    public Canvas c;

    private IQteScript[] qteScripts;

    // Use this for initialization
    void Start () {
        q.enabled = false;
        e.enabled = false;
        x.enabled = false;
        c.enabled = false;
        qteScripts = (QTEScript[])FindObjectsOfType(typeof(QTEScript));
        foreach(var s in qteScripts)
            s.PropertyChanged += QteScript_PropertyChanged;
    }

    private void QteScript_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        var qteScript = (QTEScript)sender;
        this.SetImage(qteScript.CurrentChar);
    }

    // Update is called once per frame
    void Update () {
        Vector3 namePos = Camera.main.WorldToScreenPoint(this.transform.position);
        
	}

    public void SetImage(string image)
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
