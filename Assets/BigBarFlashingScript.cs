using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BigBarFlashingScript : MonoBehaviour {

    public Slider bigBar;
    public RawImage glow;
    bool shouldFlash = false;
    private float initialValue = 0;

	void Start () {
		if(bigBar == null)
        {
            bigBar = this.GetComponentInParent<Slider>();
        }
        ChangeTransparency(0);
        //bigBar.onValueChanged.AddListener(handleValueChanged);
        this.StartCoroutine(Flash());
    }

    protected void ChangeTransparency(float value)
    {
        var tempCol = glow.color;
        tempCol.a = value;
        glow.color = tempCol;
    }
    protected float Balance(float v)
    {
        return Mathf.Sin(v);
    }
    protected IEnumerator Flash()
    {
        float c = 0;
        while (true)
        {
            if (shouldFlash)
            {
                Debug.Log("Flashing ... ");
                c += Time.deltaTime * 5;
                if (c >= 3.14)
                    c = 0;
                ChangeTransparency(Balance(c));
            }
            else
            {
                if (c != 0)
                    c = 0;
            }
            yield return null;
        }
    }
    private void handleValueChanged(float value)
    {
        if(value > initialValue)
        {
            if(shouldFlash == false)
            {
                shouldFlash = true;

                Debug.Log("Started flashing ... ");
            }
        }
        else
        {
            if (shouldFlash == true)
            {
                shouldFlash = false;
                initialValue = bigBar.value;
                ChangeTransparency(0);
            }
        }
        initialValue = value;
    }


    void Update ()
    {
        handleValueChanged(bigBar.value);
	}
}
