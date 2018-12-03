using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerLossUi : MonoBehaviour {

    public Canvas PlayerLoss;
    public PlayerPlate pp;
    public TextMeshProUGUI txt;
    PlayerCollisionHandler plch;
    float time = 0;
    int ordersize;
	// Use this for initialization
	void Start () {
        PlayerLoss.enabled = false;
        
	}
	
	// Update is called once per frame
	void Update () {
        ordersize = pp.getItemCount();
		if(plch == null)
        {
            plch = FindObjectOfType<PlayerCollisionHandler>();
            plch.OnCollisionC += OnCollisionCustomer;
            plch.OnCollisionI += OnCollisionItem;
        }
        if(PlayerLoss.enabled == true)
        {
            if (wait2s()) PlayerLoss.enabled = false;
        }

	}

    void OnCollisionCustomer()
    {
        if(ordersize>pp.getItemCount())
        {
            txt.text = "Lost all :(";
            PlayerLoss.enabled = true;
        }
        
    }

    void OnCollisionItem()
    {
        if (ordersize > pp.getItemCount())
        {
            txt.text = "-1";
            PlayerLoss.enabled = true;
        }
    }

    bool wait2s()
    {
        time += Time.deltaTime;
        if (time >= 2.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }
}
