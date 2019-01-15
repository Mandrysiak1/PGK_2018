using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Assets.PGKScripts.Enums;

public class Perkele : MonoBehaviour
{
    public MainScript mainScript;
    float time = 0f;
    [SerializeField]
    private List<string> Messages;
    private int correntMessage = 0;
    public Image background;
    public Image XboxKey;
    public Text textField;
    public Image image;
    public TextMeshProUGUI E;
    string message;
    public float letterPaused = 0.1f;
    private Canvas ui;
    private GameObject TableTrigger;
    private GameObject BarTrigger;
    public bool activated;
    bool flag;
    Coroutine coroutine1;
    // Use this for initialization


    public void MyStart()
    {
        background.enabled = false;
        XboxKey.enabled = false;
        E.enabled = true;
        image.enabled = false;
        textField.enabled = false;
        activated = true;
    }

    void Update()
    {
        if(mainScript.CurrentGameState==GameState.Playing)
        {
            if (activated)
            {
                if (ui == null)
                    ui = GameObject.Find("MainCanvas").GetComponent<Canvas>();
                if (TableTrigger == null && message != "Ready? GO GO GO RUN FOREST!")
                {
                    TableTrigger = GameObject.Find("Table_trigger");
                    if (TableTrigger != null)
                    {
                        TableTriggerTutorial TTT = TableTrigger.GetComponent<TableTriggerTutorial>();
                        GlowingScriptTutorial GST = TableTrigger.GetComponent<GlowingScriptTutorial>();
                        TableTrigger.SetActive(false);
                        GST.hasPlayer = false;
                        foreach (Outline o in GST.outlines)
                        {
                            o.enabled = false;
                        }
                        TTT.PlayerInRange = false;
                        TTT.PressE.enabled = false;
                    }

                }
                if (BarTrigger == null && message != "Ready? GO GO GO RUN FOREST!")
                {
                    BarTrigger = GameObject.Find("BarScript");
                    if (BarTrigger != null)
                    {
                        BarScriptTutorial BST = BarTrigger.GetComponent<BarScriptTutorial>();
                        GlowingScriptTutorial BGST = BarTrigger.GetComponent<GlowingScriptTutorial>();
                        BarTrigger.SetActive(false);
                        BGST.hasPlayer = false;
                        foreach (Outline o in BGST.outlines)
                        {
                            o.enabled = false;
                        }
                        BST.hasPlayer = false;
                    }

                }


                if (waiths() && flag == false)
                {
                    //Time.timeScale = 0;
                    flag = true;
                    //Message will display will be at Text
                    message = Messages[0];
                    //Set the text to be blank first
                    textField.text = "";
                    background.enabled = true;
                    XboxKey.enabled = true;
                    image.enabled = true;
                    textField.enabled = true;
                    //ui.enabled = false;
                    //Call the function and expect yield to return
                    coroutine1 = StartCoroutine(TypeText());
                }
                if (Input.GetButtonDown("Submit") && flag == true)
                {
                    correntMessage++;
                    if (Input.GetButtonDown("Submit") && flag == true && textField.text == message)
                    {
                        TableTrigger.SetActive(true);

                        BarTrigger.SetActive(true);
                        background.enabled = false;
                        XboxKey.enabled = false;
                        E.enabled = false;
                        //ui.enabled = true;
                        image.enabled = false;
                        textField.enabled = false;
                        activated = false;
                        Time.timeScale = 1;
                    }
                    else
                    if (correntMessage <= Messages.Count)
                    {
                        StopCoroutine(coroutine1);
                        textField.text = Messages[0];
                        //message = Messages[correntMessage];
                        //coroutine1 = StartCoroutine(TypeText());
                    }
                }
            }
        }
        
        
    }

    IEnumerator TypeText()
    {
        char[] letters = message.ToCharArray();
        for(int i = 0; i < message.Length; i++)
        {
            if (mainScript.CurrentGameState == GameState.Playing)
            {
                //Add 1 letter each
                textField.text += letters[i];
                yield return 0;
                yield return new WaitForSecondsRealtime(letterPaused);
            }
            else
            {
                i--;
                yield return 0;
                yield return new WaitForSecondsRealtime(letterPaused);
            }
        }
        
    }
    
    public void DisableIt()
    {
        background.enabled = false;
        XboxKey.enabled = false;
        E.enabled = false;
        //if(ui!=null)
        //ui.enabled = true;
        TableTrigger.SetActive(true);

        BarTrigger.SetActive(true);
        image.enabled = false;
        textField.enabled = false;
        Time.timeScale = 1;
    }

    public void DisableDisabling()
    {
        TableTrigger.SetActive(true);

        BarTrigger.SetActive(true);
    }

    public void DisableCanvas()
    {
        background.enabled = false;
        XboxKey.enabled = false;
        E.enabled = false;
        image.enabled = false;
        textField.enabled = false;
        Time.timeScale = 1;
    }

    bool waiths()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            time = 0;
            return true;
        }
        return false;

    }
}

