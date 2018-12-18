using System.Collections;
using System.Collections.Generic;
using Assets.PGKScripts.Enums;
using UnityEngine;
using UnityEngine.UI;

public class tutorial : MonoBehaviour {

    public Canvas KeysCanvas;
    public BarScriptTutorial bst;
    public OrderSource orderSource;
    public OrderCondition orderCondition;
    public MonoWinStreakSource winstreak;
    public ActivationOrderCondition gimmebeer;
    public PlayerPlate playerplate;
    public Text barExplain;
    public ObstacleGenEventHandlerTutorial gen;
    public MainScript ms;
    public Image bg;
    public Image bartender;
    public Text bartenderspeech;
    private float time=0;
    public AudioSource itsa_me;
    private bool[] flags = new bool[99];

    void Start ()
    {
        //barExplain.enabled = false;
        KeysCanvas.enabled = true;
        //bg.enabled = false;
        /*orderSource.Mood = 0;
        gimmebeer.sw = true;*/
	}

	void Update ()
    {
        if(flags[0] == false)
        {
            if (Input.GetAxis("Horizontal") > 0f || Input.GetAxis("Vertical") > 0f)
            {
                flags[0] = true;
            }
        }
        if (flags[0] == true && flags[1] == false)
        {
            if (wait10s(ref time))
            {
                KeysCanvas.enabled = false;
                bg.enabled = false;
                bartender.enabled = false;
                bartenderspeech.enabled = false;
                flags[1] = true;
            }

        }
        if (flags[1] == true && flags[2] == false)
        {
            bg.enabled = true;
            bartender.enabled = true;
            bartenderspeech.text = "Come over to the bar and pick up 5 beers. You can put down the beer by pressing 'R' or 'X' on gamepad if you ever need to.";
            bartenderspeech.enabled = true;
            flags[2] = true;
        }
        if(flags[2]==true&&flags[3]==false)
        {
            ManageBarCanvas();
            if (playerplate.getItemCount() >= 5)
            {
                DisableBarCanvas();
                flags[3] = true;
                bartenderspeech.text = "Great. Now deliver the beer to our customers. Maybe you'll get a tip if they're nice. You can spend it on various upgrades after completing a level. Just don't bump into anyone, everything will fall off your plate.";

                FirstOrder();
            }
        }
        if (flags[3] == true && flags[4] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder != null) flags[4] = true;
        }
        if (flags[4] == true && flags[5] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder == null)
            {
                DisableBarCanvas();
                flags[5] = true;
                winstreak.WinStreak += 1;
            }
            
        }
        if (flags[5] == true && flags[6] == false)
        {
            flags[6] = true;
            ManageBarCanvas();
            bartenderspeech.text = "The guests start getting upset if you make them wait for too long. Their mood is represented by emoji floating above them. We're just practicing though, so don't worry. Go again. Practice makes perfect, right?";
            FirstOrder();
        }
        if (flags[6] == true && flags[7] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder != null) flags[7] = true;
        }
        if (flags[7] == true && flags[8] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder == null)
            {
                DisableBarCanvas();
                flags[8] = true;
                winstreak.WinStreak += 14;
            }
        }
        if (flags[8]== true && flags[9] == false)
        {
            flags[9] = true;
            bartenderspeech.text = "You have acquired your first winstreak! You receive 1 point after completing orders without pissing off our guests. Use one now. Or more if you want. Up to you.";
        }
        if(flags[9] == true && flags[10] == false)
        {
            if (Input.GetButton("Perk_1") || Input.GetButton("Perk_2") || Input.GetButton("Perk_3"))  
            {
                bartenderspeech.text = "Fast as sanic and strong as Pudzian. Remember though, with great power comes great responsibility. Now now, deliver the beer, the clients are waiting.";
                ManageBarCanvas();
                FirstOrder();
                flags[10] = true;
            }
        }
        if (flags[10] == true && flags[11] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder != null) flags[11] = true;
        }
        if (flags[11] == true && flags[12] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder == null)
            {
                DisableBarCanvas();
                flags[12] = true;
                bartenderspeech.text = "Looks like you're cut out for it, damn. There's one final task before I'll give you my Seal of Approval\u2122"+".";
            }
        }
        if(flags[12]==true && flags[13]==false)
        {
            if (wait5s(ref time)) flags[13] = true;
        }
        if(flags[13]==true && flags[14]==false)
        {
            spawnObject();
            bartenderspeech.text = "Oh shoot! Looks like someone dropped a frying pan over there! Where did they even get it from? You'll drop one beer if you slip on it, so watch out.";
            flags[14] = true;
        }
        if (flags[14] == true && flags[15] == false)
        {
            if (wait15s(ref time))
            {
                bartenderspeech.text = "Oh right. The \"final task\". Let's see how you doin' it under pressure. When clients are angry, the bar at the top fills up. When it fills, we ALL DIE. Total mayhem. I'll give you some time to prepare for it.";
                flags[15] = true;
            }
        }
        if(flags[15]==true && flags[20]==false)
        {
            if (wait12s(ref time)) flags[20] = true;
        }
        if(flags[20]==true && flags[21] == false)
        {
            itsa_me.Play();
            flags[21] = true;
        }
        if(flags[21]==true && flags[16] == false)
        {
            if (wait4s(ref time)) flags[16] = true;
        }
        if(flags[16]==true && flags[17]==false)
        {
            ManageBarCanvas();
            bartenderspeech.text = "Ready? GO GO GO RUN FOREST";
            LastOrder();
            flags[17] = true;
        }
        if (flags[17] == true && flags[18] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder != null) flags[18] = true;
        }
        if (flags[18] == true && flags[19] == false)
        {
            ManageBarCanvas();
            if (orderSource.CurrentOrder == null)
            {
                winstreak.WinStreak += 1;
                DisableBarCanvas();
                flags[19] = true;
                bartenderspeech.text = "We're alive! Good job. You're ready for the real deal. Peak hours with angry people demanding alcohol. 2 minutes. What could go wrong aye?";
            }
        }
        if(flags[19]==true && wait10s(ref time))
        {
            bg.enabled = false;
            bartender.enabled = false;
            bartenderspeech.enabled = false;
            ms.GameOver(GameState.Success);
        }
        /*if (flags[8] == true && flags[9] == false)
        {
            if (wait5s(ref time))
            {
                LastOrder();
                flags[9] = true;
            }
        }
        if (flags[9] == true && flags[10] == false)
        {
            if (orderSource.CurrentOrder != null)
            {
                flags[10] = true;
                //barExplain.enabled = true;
            }
        }
        if (flags[11] == true)
        {
            if (orderSource.CurrentOrder == null) ms.GameOver(GameState.Success);
        }*/



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
    bool wait8s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 8.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }
    bool wait10s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 10.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }

    bool wait12s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 12.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }

    bool wait15s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 15.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }

    bool wait2s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 2.0f)
        {
            time = 0;
            return true;
        }
        return false;

    }
    bool wait4s(ref float time)
    {
        time += Time.deltaTime;
        if (time >= 4.0f)
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

    void ManageBarCanvas()
    {
        if (bst.hasPlayer == false) bst.barCanvasHelp.enabled = true;
        else bst.barCanvasHelp.enabled = false;
        if (bst.hasPlayer == true)
        {
            bst.barCanvasPickup.enabled = true;
        }
        else bst.barCanvasPickup.enabled = false;
    }

    void DisableBarCanvas()
    {
        bst.barCanvasHelp.enabled = false;
        bst.barCanvasPickup.enabled = false;
    }

}
