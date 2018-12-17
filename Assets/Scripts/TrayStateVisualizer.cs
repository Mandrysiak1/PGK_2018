using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrayStateVisualizer : MonoBehaviour {

    private OrderItem lastOrder = null;
    public PlayerPlate playerPlate;
    public WinStreak winStreaks;
    public TextMeshProUGUI ShowString;
    private int current;
    private int max;
    private string ToShow = " 0 / 0";
	void Start () {
        if (playerPlate == null)
            playerPlate = FindObjectOfType<PlayerPlate>();
        playerPlate.OnItemAmountChanged.AddListener(ChangeViewItem);
        playerPlate.OnMaximumCapacityChanged.AddListener(ChangeViewMax);
        current = playerPlate.CurrentCapacity;
        max = playerPlate.MaximumCapacity;
        SetShowString(current, max);
	}

    private void SetShowString(int current, int max)
    {
        ToShow = current.ToString() + " / " + max;
        ShowString.text = ToShow;
    }

    private void ChangeViewMax(int arg0, int arg1)
    {
        this.max = arg0;
        SetShowString(current, this.max);
        
    }

    private void ChangeViewItem(OrderItem arg0, int arg1, int arg2)
    {
        current = playerPlate.getItemCount();
        SetShowString(current, this.max);
    }

    void Update () {
		
	}
}
