using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public Canvas one;
    public Canvas two;
    public Canvas thr;
    public Canvas four;
    public Canvas five;
    public Canvas six;
    public Canvas seven;
    public Canvas eight;
    public Canvas nine;
    public TextMeshProUGUI ButtonText;
    int counter = 1;
    List<Canvas> canvases = new List<Canvas>();
    // Use this for initialization
    public void Start()
    {
        canvases.Add(one);
        canvases.Add(two);
        canvases.Add(thr);
        canvases.Add(four);
        canvases.Add(five);
        canvases.Add(six);
        canvases.Add(seven);
        canvases.Add(eight);
        canvases.Add(nine);
        for(int i = 2; i < canvases.Count; i++)
        {
            canvases[i].enabled = false;
        }
    }




    // Update is called once per frame
    public void onClick()
    {
        if (counter == 8)
        {
            ButtonText.text = "play";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            counter++;
            canvases[counter - 1].enabled = false;
            canvases[counter].enabled = true;
        }

    }
}
