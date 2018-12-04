using System.Collections;
using System.Collections.Generic;
using Assets.PGKScripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour {

    public Canvas KeysCanvas;
    public Canvas BarCanvasHelp;
    public OrderSource orderSource;
    public OrderCondition orderCondition;
    public MonoWinStreakSource winstreak;
    public ActivationOrderCondition gimmebeer;
    public Text barExplain;
    public ObstacleGenEventHandlerTutorial gen;
    public MainScript ms;
    private float time=0;
    private bool[] flags = new bool[10];
    void Start ()
    {
        barExplain.enabled = false;
        KeysCanvas.enabled = true;
        /*orderSource.Mood = 0;
        gimmebeer.sw = true;*/
	}

	void Update ()
    {
        if(flags[0] == false)
        {
            if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Vertical") > 0.1f)
            {
                flags[0] = true;
            }
        }
        if (flags[0] == true && flags[1] == false)
        {
            if (wait5s(ref time))
            {
                KeysCanvas.enabled = false;
                flags[1] = true;
            }

        }
        if (flags[1] == true && flags[2] == false)
        {
            FirstOrder();
            flags[2] = true;
        }
        if (flags[2] == true && flags[3] == false)
        {
            if (orderSource.CurrentOrder != null) flags[3] = true;
        }
        if (flags[3] == true && flags[4] == false)
        {
            if (orderSource.CurrentOrder == null) flags[4] = true;
        }
        if(flags[4] == true && flags[5] == false)
        {
            winstreak.WinStreak += 100;
            flags[5] = true;
        }
        if(flags[5] == true && flags[6] == false)
        {
            if (Input.GetButton("Perk_1") || Input.GetButton("Perk_2") || Input.GetButton("Perk_3"))
            flags[6] = true;
        }
        if (flags[6] == true && flags[7] == false)
        {
            if (wait5s(ref time))
            {
                spawnObject();
                flags[7] = true;

            }
        }
        if (flags[7] == true && flags[8] == false)
        {
            if (wait5s(ref time))
            {
                LastOrder();
                flags[8] = true;
            }
        }
        if (flags[8] == true && flags[9] == false)
        {
            if (orderSource.CurrentOrder != null)
            {
                flags[9] = true;
                barExplain.enabled = true;
            }
        }
        if (flags[9] == true)
        {
            if (orderSource.CurrentOrder == null) ms.GameOver(GameState.Success);
        }



    }

    bool wait5s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 5.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }



    void FirstOrder()
    {
        gimmebeer.sw = true;
    }

    void GiveWins()
    {
        winstreak.WinStreak += 100;
    }

    void spawnObject()
    {
        gen.GenerateObstacle();
    }


    void LastOrder()
    {
        orderSource.Mood = 0;
        gimmebeer.sw = true;

    }

}
