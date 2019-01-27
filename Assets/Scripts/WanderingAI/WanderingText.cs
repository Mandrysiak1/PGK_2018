using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WanderingText : MonoBehaviour {
    public Text text;

	void Update () {
        if(text != null && text.enabled == true)
        {
            Vector3 textPos = Camera.main.WorldToScreenPoint(this.transform.position);
            text.transform.position = textPos;
        }
	}
}
