using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets;
using Assets.PGKScripts.Enums;
public class OrderGeneratorEventHandeler : MonoBehaviour {

    Scene currentScene;
    private  MainScript mainScript;
    private bool isWitchOrderEnable;
    private int randomTable;

    public OrderGeneratorEventHandeler()
    {
        
    }

    // Use this for initialization

    void Start () {
        mainScript = FindObjectOfType<MainScript>(); ;
        isWitchOrderEnable = CheckScene();
        var x = FindObjectOfType<OrderGenerator>();
        x.OnGenerateOrder += HandleOrderGenerator;
        currentScene = SceneManager.GetActiveScene(); 
    }

    private void HandleOrderGenerator()
    {

        GenerateOrder();

    }

    private void GenerateOrder()
    {
        //randomTable = 0;
        Debug.Log(mainScript.FreeTables.Count + " freetables");

        if(mainScript.FreeTables.Count != 0)
        {
            if (isWitchOrderEnable)
            {
                
                int randomTable = UnityEngine.Random.Range(0, mainScript.FreeTables.Count);
                Debug.Log(randomTable);
            }
            else
            {
                int randomTable = UnityEngine.Random.Range(0, mainScript.FreeTables.Count - 1);
            
            }
            
            if(randomTable == mainScript.FreeTables.Count)
            {
                GenerateWitchOrder();
            }
            else
            {
                GenerateBeerOrder(randomTable);
            }
        }
    }

    private void GenerateWitchOrder()
    {
        mainScript.WitchTable.CurrOrder = new Order(mainScript.GetTime(), OrderType.WitchOrder);
        
    }

    private void GenerateBeerOrder(int randomTable)
    {
        
        mainScript.FreeTables[randomTable].CurrOrder = new Order(mainScript.GameTime, OrderType.Beer);
        mainScript.AwaitingTables.Add(mainScript.FreeTables[randomTable]);
        mainScript.FreeTables.Remove(mainScript.FreeTables[randomTable]);
    }

    //TODO: change it when scene will 
    //cointain an object with the witch script
    //and just check if scene contains it; 
    private bool CheckScene()
    {
        if(currentScene.name == "level2") 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
