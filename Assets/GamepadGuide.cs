using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamepadGuide : ButtonVisualization
{
    private Vector3 initScale;

    private void Start()
    {
        initScale = gameObject.transform.localScale;
    }

    protected override void Refresh()
    {
        if(GamepadMode)
        {
            gameObject.transform.localScale = initScale;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}