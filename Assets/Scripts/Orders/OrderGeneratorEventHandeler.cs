using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets;
using Assets.PGKScripts.Enums;
public class OrderGeneratorEventHandeler : MonoBehaviour {

    
    private  MainScript mainScript;

    private int randomTable;


    // Use this for initialization

    void Start () {
        mainScript = FindObjectOfType<MainScript>(); ;
       
        var x = FindObjectOfType<OrderGenerator>();
        x.OnGenerateBeerOrder += HandleBeerOrder;
        x.OnGenerateWitchOrderEvent += HandleWitchOrder;
       
    }

    private void HandleWitchOrder()
    {
        GenerateWitchOrder();
    }

    private void HandleBeerOrder()
    {

        GenerateOrder();

    }

    private void GenerateOrder()
    {
        Debug.Log(mainScript.FreeTables.Count + " freetables");

        if(mainScript.FreeTables.Count != 0)
        {
             int randomTable = UnityEngine.Random.Range(0, mainScript.FreeTables.Count - 1);
            
             GenerateBeerOrder(randomTable);

        }
    }

    private void GenerateWitchOrder()
    {
        if(mainScript.WitchTable.CurrOrder == null)
        {
            mainScript.WitchTable.CurrOrder = new Order(mainScript.GetTime(), OrderType.WitchOrder);
            Debug.Log("WYGENEROWANO ZAMÓWIENIE WIEDZMY");
        }

        
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
   
}
