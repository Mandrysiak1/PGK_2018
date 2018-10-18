using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIupdater : MonoBehaviour
{

    public Slider slider;
    float Satisfaction;
    public Canvas one;
    public Canvas two;
    public Canvas three;
    public Canvas four;
    public Canvas five;
    public Canvas beer;
    public Text howmany;
    public void StartFillingUpBar()
    {
        slider.value = 0;
        StartCoroutine(FillBarAsync());
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
                StartCoroutine(FillBarAsync());
                break;
            }
            yield return null;
        }
    }

}
