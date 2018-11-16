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


    void Start () {
        mainScript = FindObjectOfType<MainScript>(); ;
       
        var x = FindObjectOfType<OrderGenerator>();
    

       
    }


   
}
