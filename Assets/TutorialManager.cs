using QTE;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    private bool welcome = false;
    private bool ordergenerated = false;
    private bool ordercompleted = false;
    private bool ordersexplained = false;
    private bool generateExplained=false;
    private bool itemcollisionExplained = false;
    private bool clientcollisionexplained = false;
    private bool winstreakExplained = false;
    private bool doit = false;
    //private bool tipsExplained = false;
    private bool bullshitone = true;
    private bool bullshittwo = true;
    private bool qteexplained1 = false;
    private bool qteexplained2 = false;
    private float timer;
    public MainScript mainScript;
    public QTEController qtescript;
    public PlayerCollisionHandler collisionHandler;
    public Canvas bg;
    public Canvas Welcome1;
    public Canvas Welcome2;
    public Canvas Beer;
    public Canvas TableSat;
    public Canvas RedBarOfDoom;
    public Canvas Orders1;
    public Canvas Orders2;
    public Canvas tips;
    public Canvas Winstreak1;
    public Canvas Winstreak2;
    public Canvas QTE1;
    public Canvas QTE2;
    public Canvas ItemObstacle;
    public Canvas BumpIntoCustomer;
    public Canvas ObstacleGenCanv;
    public Canvas Tut;
    public GameObject Tutall;
    public PlayerPlate PlayerPlate;
    public OrderItem orderToPick;
    private Table AwaitingOne;
    public void Update()
    {


        fillObjects();
        checkorders();

        if (welcome == false)
        {
            Time.timeScale = 0;    
        }
        
            
        if (welcome == true && ordergenerated == true && ordersexplained == false)
        {
            Time.timeScale = 0;
            bg.gameObject.SetActive(true);
            Orders1.gameObject.SetActive(true);
            
            
        }

        if (ordercompleted == true && winstreakExplained == false)
        {
            Time.timeScale = 0;
            bg.gameObject.SetActive(true);
            Winstreak1.gameObject.SetActive(true);
        }
        
        if (qteexplained1 == false && winstreakExplained == true)
        {
            if(tajm())
            {
                Time.timeScale = 0;
                timer = 0;
                bg.gameObject.SetActive(true);
                QTE1.gameObject.SetActive(true);
            }
            

        }
        




        if (doit == false && welcome && ordergenerated && ordersexplained && (generateExplained || itemcollisionExplained || clientcollisionexplained) && winstreakExplained && qteexplained2 && !qtescript.IsRunning)
        {

            Time.timeScale = 0;
            doit = true;
            bg.gameObject.SetActive(true);
            Tut.gameObject.SetActive(true);
            
        }

    }

    public bool tajm()
    {
        timer += Time.deltaTime;
        if (timer <= 10) return false;
        else return true;

    }

    public void fillObjects()
    {
        if (bullshitone)
        {
            var ogen = FindObjectOfType<ObstacleGenerator>();
            if (ogen != null)
            {
                ogen.OnGenerateObstacle += HandleObstacleGen;
                bullshitone = false;
            }

        }

        if (bullshittwo)
        {
            var col = FindObjectOfType<PlayerCollisionHandler>();
            if (col != null)
            {
                col.OnCollisionC += HandleCollisionClient;
                col.OnCollisionI += HandleCollisionItem;
                bullshittwo = false;
            }
        }
    }
    public void checkorders()
    {
        if (ordergenerated == false)
        {
            if (mainScript.AwaitingTables.Count > 0)
            {
                ordergenerated = true;
                AwaitingOne = mainScript.AwaitingTables[0];
            }
        }

        if (ordergenerated == true)
        {
            if (!mainScript.AwaitingTables.Contains(AwaitingOne))
            {
                ordercompleted = true;
            }
        }
    }

    public void HandleObstacleGen()
    {
        if(generateExplained == false && Tutall.active)
        {
            Time.timeScale = 0;
            bg.gameObject.SetActive(true);
            ObstacleGenCanv.gameObject.SetActive(true);
            
            
        }
    }

    public void HandleCollisionItem()
    {
        if (itemcollisionExplained == false && Tutall.active)
        {
            Time.timeScale = 0;
            bg.gameObject.SetActive(true);
            ItemObstacle.gameObject.SetActive(true);
            
        }
    }
     
    public void HandleCollisionClient()
    {
        if (clientcollisionexplained == false && Tutall.active)
        {
            Time.timeScale = 0;
            bg.gameObject.SetActive(true);
            BumpIntoCustomer.gameObject.SetActive(true);
        }
    }


    public void NextW1()
    {
        Debug.Log(bullshitone);
        Welcome1.gameObject.SetActive(false);
        Welcome2.gameObject.SetActive(true);
    }
    public void NextW2()
    {
        Welcome2.gameObject.SetActive(false);
        Beer.gameObject.SetActive(true);
        Debug.Log(bullshitone);
    }

    public void NextB()
    {
        Beer.gameObject.SetActive(false);
        TableSat.gameObject.SetActive(true);
    }

    public void NextT()
    {
        TableSat.gameObject.SetActive(false);
        RedBarOfDoom.gameObject.SetActive(true);
    }

    public void NextBar()
    {
        Time.timeScale = 1;
        welcome = true;
        RedBarOfDoom.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
        //mainScript.GetPlayer().AddOrderItemOnPlate(orderToPick);

    }

    public void NextOrd()
    {
        Orders1.gameObject.SetActive(false);
        Orders2.gameObject.SetActive(true);
        ordersexplained = true;
    }

    public void NextOrd2()
    {
        Orders2.gameObject.SetActive(false);
        tips.gameObject.SetActive(true);
        
    }

    public void NextTip()
    {
        Time.timeScale = 1;
        tips.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }

    public void NextWin1()
    {
        winstreakExplained = true;
        Winstreak1.gameObject.SetActive(false);
        Winstreak2.gameObject.SetActive(true);
    }

    public void NextWin2()
    {
        Time.timeScale = 1;
        Winstreak2.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
        mainScript.WinStreak += 2;
    }

    public void NextQTE1()
    {
        qteexplained1 = true;
        QTE1.gameObject.SetActive(false);
        QTE1.gameObject.SetActive(false);
        QTE2.gameObject.SetActive(true);
    }

    public void NextQTE2()
    {
        
        Time.timeScale = 1;
        QTE2.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
        if(mainScript.GetPlayer().GetItemOrderOnPlateQuantity(orderToPick) < 4)
        {
            mainScript.GetPlayer().AddOrderItemOnPlate(orderToPick);
            mainScript.GetPlayer().AddOrderItemOnPlate(orderToPick);
        }
        qtescript.TryRunCollisionQte();
        qteexplained2 = true;
    }

    public void NextClient()
    {
        Time.timeScale = 1;
        clientcollisionexplained = true;
        BumpIntoCustomer.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }

    public void NextItem()
    {
        Time.timeScale = 1;
        itemcollisionExplained = true;
        ItemObstacle.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }

    public void NextObst()
    {
        Time.timeScale = 1;
        generateExplained = true;
        ObstacleGenCanv.gameObject.SetActive(false);
        bg.gameObject.SetActive(false);
    }

    public void Last()
    {
        doit = true;
        Time.timeScale = 1;
        bg.gameObject.SetActive(false);
        Tut.gameObject.SetActive(false);
        Tutall.SetActive(false);
        
    }

}
