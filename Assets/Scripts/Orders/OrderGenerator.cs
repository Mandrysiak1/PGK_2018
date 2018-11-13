using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderGenerator : MonoBehaviour {


    public delegate void GenerateOrderEvent();
    public event GenerateOrderEvent OnGenerateOrder;

    private  MainScript mainScript;
    private float nextOrderTime;


    void Start () {

        mainScript = FindObjectOfType<MainScript>();
        CalculateNextOrderTime();
    }
	

	void Update () {
        if(CheckIfOrderNeeded())
        {
            GenerateOrder();
        }

       

    }

    private bool CheckIfOrderNeeded()
    {
        if (mainScript.GameTime >= nextOrderTime)
        {
            CalculateNextOrderTime();
            return true;

        }else
        {
            return false;
        }
    }

    void CalculateNextOrderTime()
    {
        int offset = UnityEngine.Random.Range(3,7);
        nextOrderTime = mainScript.GameTime + offset;
        //TODO:Wymyśleć jakiś sposób na zmiane czasu;
    }

    private void GenerateOrder()
    {
        if(OnGenerateOrder != null)
        {
            OnGenerateOrder();
        }
    }
}
