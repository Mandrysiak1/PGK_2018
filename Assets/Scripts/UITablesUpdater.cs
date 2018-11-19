using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITablesUpdater : MonoBehaviour
{
    public TableScript tableScript;
    public MainScript mainScript;
    //public Slider slider;
    public Canvas one;
    public Canvas two;
    public Canvas three;
    public Canvas four;
    public Canvas five;
    public Canvas beer;
    public Text howmany;
    private List<Canvas> ListOfCanvases = new List<Canvas>();
    private void Start()
    {
        beer.enabled = false;
        ListOfCanvases.Add(one);
        ListOfCanvases.Add(two);
        ListOfCanvases.Add(three);
        ListOfCanvases.Add(four);
        ListOfCanvases.Add(five);
        CanvasEnabler(one);
    }

    void Update()
    {
        if (tableScript.MyTable.IsThereOrder())
        {
            howmany.text = "x " + (tableScript.MyTable.CurrOrder.getOrderSize() - tableScript.MyTable.beersOnTable);
            beer.enabled = true;
        }
        else beer.enabled = false;
        if (tableScript.MyTable.Mood >= 16)
        {
            CanvasEnabler(one);
        }
        if (tableScript.MyTable.Mood >= 12 && tableScript.MyTable.Mood < 16)
        {
            CanvasEnabler(two);
        }
        if (tableScript.MyTable.Mood >= 8 && tableScript.MyTable.Mood < 12)
        {
            CanvasEnabler(three);
        }
        if (tableScript.MyTable.Mood >= 4 && tableScript.MyTable.Mood < 8)
        {
            CanvasEnabler(four);
        }
        if (tableScript.MyTable.Mood < 4)
        {
            CanvasEnabler(five);

        }
    }

    void CanvasEnabler(Canvas canvas)
    {
        foreach(Canvas c in ListOfCanvases)
        {
            if (c == canvas) c.enabled = true;
            else c.enabled = false;
        }
    }
   
}
