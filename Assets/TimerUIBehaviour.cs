using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIBehaviour : MonoBehaviour {

    public WinGameWhenTimeIsOver timer;
    public Image glowIcon;
    public Image icon;
    public float timerPulsateTime = 20;
    private bool bloomInitialized = false;

    private Coroutine bloomCoroutine;

	// Use this for initialization
	void Start () {
        if (timer == null)
            timer = FindObjectOfType<WinGameWhenTimeIsOver>();
        glowIcon.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(timer.Limit - timer.Timer) <= timerPulsateTime)
        {
            if(!bloomInitialized)
            {
                glowIcon.enabled = true;
                bloomCoroutine = StartCoroutine(BloomEffect());
            }
        }
        else
        {
            if (bloomInitialized)
            {
                glowIcon.enabled = false;
                StopCoroutine(bloomCoroutine);
            }
        }
        icon.fillAmount = Mathf.Abs(timer.Limit - timer.Timer) / timer.Limit;
        glowIcon.fillAmount = Mathf.Abs(timer.Limit - timer.Timer) / timer.Limit;

    }

    protected IEnumerator BloomEffect()
    {
        bloomInitialized = true;
        float c = 0;
        while (true)
        {
                c += Time.deltaTime * 5;
                if (c >= 3.14)
                    c = 0;
                ChangeTransparency(Balance(c));
            yield return null;
        }
    }

    protected void ChangeTransparency(float value)
    {
        var tempCol = glowIcon.color;
        tempCol.a = value;
        glowIcon.color = tempCol;
    }
    protected float Balance(float v)
    {
        return Mathf.Sin(v);
    }
}
