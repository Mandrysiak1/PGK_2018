using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIupdater : MonoBehaviour
{
    public TableScript tableScript;
    //public Slider slider;
    public Canvas one;
    public Canvas two;
    public Canvas three;
    public Canvas four;
    public Canvas five;
    public Canvas beer;
    public Text howmany;

    private void Start()
    {
        if (one.enabled == false) one.enabled = true;
        if (two.enabled == true) two.enabled = false;
        if (three.enabled == true) three.enabled = false;
        if (four.enabled == true) four.enabled = false;
        if (five.enabled == true) five.enabled = false;
        if (beer.enabled == true) beer.enabled = false;
    }

    void Update()
    {
        if (tableScript.MyTable.IsThereOrder() == true)
        {
            howmany.text = "x " + (tableScript.MyTable.CurrOrder.getOrderSize() - tableScript.MyTable.beersOnTable);
            if (beer.enabled == false) beer.enabled = true;
        }
        else beer.enabled = false;
        if (tableScript.MyTable.Mood >= 16)
        {
            if (one.enabled == false) one.enabled = true;
            if (two.enabled == true) two.enabled = false;
            if (three.enabled == true) three.enabled = false;
            if (four.enabled == true) four.enabled = false;
            if (five.enabled == true) five.enabled = false;
        }
        if (tableScript.MyTable.Mood >= 12 && tableScript.MyTable.Mood < 16)
        {
            if (one.enabled == true) one.enabled = false;
            if (two.enabled == false) two.enabled = true;
            if (three.enabled == true) three.enabled = false;
            if (four.enabled == true) four.enabled = false;
            if (five.enabled == true) five.enabled = false;
        }
        if (tableScript.MyTable.Mood >= 8 && tableScript.MyTable.Mood < 12)
        {
            if (one.enabled == true) one.enabled = false;
            if (two.enabled == true) two.enabled = false;
            if (three.enabled == false) three.enabled = true;
            if (four.enabled == true) four.enabled = false;
            if (five.enabled == true) five.enabled = false;
        }
        if (tableScript.MyTable.Mood >= 4 && tableScript.MyTable.Mood < 8)
        {
            if (one.enabled == true) one.enabled = false;
            if (two.enabled == true) two.enabled = false;
            if (three.enabled == true) three.enabled = false;
            if (four.enabled == false) four.enabled = true;
            if (five.enabled == true) five.enabled = false;
        }
        if (tableScript.MyTable.Mood < 4)
        {
            if (one.enabled == true) one.enabled = false;
            if (two.enabled == true) two.enabled = false;
            if (three.enabled == true) three.enabled = false;
            if (four.enabled == true) four.enabled = false;
            if (five.enabled == false) five.enabled = true;
            //StartCoroutine(FillBarAsync());
        }
    }

    /*public void StartFillingUpBar()
    {
        //slider.value = 0;
        StartCoroutine(FillBarAsync());
    }

    public void StopFillingUpBar()
    {
        StopCoroutine(FillBarAsync());
    }

    public void StartTestOrder()
    {
        Satisfaction = 10;
        StartCoroutine(TestOrder());
    }

    IEnumerator FillBarAsync()
    {
        while (slider.value != 100)
        {
            slider.value += Time.deltaTime;
            yield return null;
        }
    }

    void EnableImages()
    {
        howmany.text = "x " + 3;
        howmany.enabled = true;
    }

    IEnumerator TestOrder()
    {
        howmany.text = "x " + 3;
        beer.enabled = true;
        //beer.gameObject.SetActive(true);
        while (true)
        {
            Satisfaction -= Time.deltaTime;
            if (Satisfaction >= 8)
            {
                if (one.enabled == false) one.enabled = true;
                if (two.enabled == true) two.enabled = false;
                if (three.enabled == true) three.enabled = false;
                if (four.enabled == true) four.enabled = false;
                if (five.enabled == true) five.enabled = false;
            }
            if (Satisfaction >= 6 && Satisfaction < 8)
            {
                if (one.enabled == true) one.enabled = false;
                if (two.enabled == false) two.enabled = true;
                if (three.enabled == true) three.enabled = false;
                if (four.enabled == true) four.enabled = false;
                if (five.enabled == true) five.enabled = false;
            }
            if (Satisfaction >= 4 && Satisfaction < 6)
            {
                if (one.enabled == true) one.enabled = false;
                if (two.enabled == true) two.enabled = false;
                if (three.enabled == false) three.enabled = true;
                if (four.enabled == true) four.enabled = false;
                if (five.enabled == true) five.enabled = false;
            }
            if (Satisfaction >= 2 && Satisfaction < 4)
            {
                if (one.enabled == true) one.enabled = false;
                if (two.enabled == true) two.enabled = false;
                if (three.enabled == true) three.enabled = false;
                if (four.enabled == false) four.enabled = true;
                if (five.enabled == true) five.enabled = false;
            }
            if (Satisfaction >= 0 && Satisfaction < 2)
            {
                if (one.enabled == true) one.enabled = false;
                if (two.enabled == true) two.enabled = false;
                if (three.enabled == true) three.enabled = false;
                if (four.enabled == true) four.enabled = false;
                if (five.enabled == false) five.enabled = true;
                //StartCoroutine(FillBarAsync());
                break;
            }
            yield return null;
        }
    }*/

}
