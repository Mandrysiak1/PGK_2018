using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopPapyrus : MonoBehaviour {

    [SerializeField]
    private TextMeshProUGUI speed;
    [SerializeField]
    private TextMeshProUGUI capacity;
    private GameObject gameManager;
    private GameObject player;
    // Use this for initialization
    void Start () {
        


    }
	
	// Update is called once per frame
	void Update () {
        float speedo = 0.85f + 0.25f * UpgradeClass.SpeedModif;
        int descapacito = 5 + UpgradeClass.BeerModif;
        float rounded = (float)(Math.Round((double)speedo, 2));
        speed.SetText("Movement speed: "+ rounded);
        capacity.SetText("Tray capacity: " + descapacito);
    }
}
