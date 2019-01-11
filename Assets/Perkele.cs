using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Perkele : MonoBehaviour
{
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
    bool activated;
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
        if(activated)
        {
            if (ui == null)
                ui = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            if (TableTrigger == null && message != "Ready? GO GO GO RUN FOREST!")
            {
                TableTrigger = GameObject.Find("Table_trigger");
                if (TableTrigger != null)
                {
                    TableTriggerTutorial TTT = TableTrigger.GetComponent<TableTriggerTutorial>();

                    TableTrigger.SetActive(false);
                    TTT.PlayerInRange = false;
                    TTT.PressE.enabled = false;
                }
                
            }
            if (BarTrigger == null && message != "Ready? GO GO GO RUN FOREST!")
            {
                BarTrigger = GameObject.Find("BarScript");
                if(BarTrigger != null)
                {
                    BarScriptTutorial BST = BarTrigger.GetComponent<BarScriptTutorial>();

                    BarTrigger.SetActive(false);
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

    IEnumerator TypeText()
    {

        foreach (char letter in message.ToCharArray())
        {
            //Add 1 letter each
            textField.text += letter;
            yield return 0;
            yield return new WaitForSecondsRealtime(letterPaused);
        }
    }
    
    public void DisableIt()
    {
        background.enabled = false;
        XboxKey.enabled = false;
        E.enabled = false;
        if(ui!=null)
        //ui.enabled = true;
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

