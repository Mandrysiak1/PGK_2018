using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OrderGenerator : MonoBehaviour
{


    private bool isWitchOrderEnable;

    public delegate void GenerateBeerOrderEvent();
    public event GenerateBeerOrderEvent OnGenerateBeerOrder;

    public delegate void GenerateWitchOrderEvent();
    public event GenerateWitchOrderEvent OnGenerateWitchOrderEvent;

    private MainScript mainScript;
    private float nextOrderTime;


    void Start()
    {
      

            //currentScene = SceneManager.GetActiveScene();
            //isWitchOrderEnable = CheckScene();
            mainScript = FindObjectOfType<MainScript>();
            CalculateNextOrderTime();
            //  Debug.Log(isWitchOrderEnable.ToString());

        
    }



    void Update()
    {
        isWitchOrderEnable = CheckScene(); //TODO: to change, cant do it better now
       // Debug.Log(isWitchOrderEnable);
        BeerOrder();
        WitchOrder();
    }

    private void WitchOrder()
    {
        if(isWitchOrderEnable == true)
        {
            if (UnityEngine.Random.Range(0, 100) < 10) //TODO: change it, its just for testing 
            {
                if (OnGenerateWitchOrderEvent != null)
                {
                    OnGenerateWitchOrderEvent();
                }
            }
        }

    }

    private void BeerOrder()
    {
        if (CheckIfOrderNeeded())
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

        }
        else
        {
            return false;
        }
    }

    void CalculateNextOrderTime()
    {
        int offset = UnityEngine.Random.Range(3, 7);
        nextOrderTime = mainScript.GameTime + offset;
        //TODO:Wymyśleć jakiś sposób na zmiane czasu;
    }

    private void GenerateOrder()
    {
        if (OnGenerateBeerOrder != null)
        {
            OnGenerateBeerOrder();
        }
    }

    private bool CheckScene() //TODO: to change
    {
        bool val = false;
        Scene[] scenes = new Scene[SceneManager.sceneCount];
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            scenes[i] = SceneManager.GetSceneAt(i);
     

            if (scenes[i].name == "level2")
            {
                val = true;

            }
     
        }       
        return val;
    }
}
