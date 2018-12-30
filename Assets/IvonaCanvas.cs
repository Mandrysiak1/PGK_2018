using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class IvonaCanvas : MonoBehaviour {
    float time = 0f;
    [SerializeField]
    private List<string> Messages;
    private int correntMessage = 0;
    public Image background;
    public Image black;
    public Image XboxKey;
    public Text textField;
    public Image image;
    public TextMeshProUGUI E;
    string message;
    public float letterPaused = 0.1f;
    private Canvas ui;
    bool flag;
    Coroutine coroutine1;
    // Use this for initialization
    void Start()
    {
        background.enabled = false;
        XboxKey.enabled = false;
        E.enabled = true;
        image.enabled = false;
        textField.enabled = false;



    }

    void Update()
    {

        if(ui == null)
            ui = GameObject.Find("MainCanvas").GetComponent<Canvas>();

        if (waiths() && flag == false)
        {
            Time.timeScale = 0;
            flag = true;
            //Message will display will be at Text
            message = Messages[0];
            //Set the text to be blank first
            textField.text = "";
            background.enabled = true;
            XboxKey.enabled = true;
            image.enabled = true;
            textField.enabled = true;
            black.enabled = true;
            ui.enabled = false;
            //Call the function and expect yield to return
            coroutine1 = StartCoroutine(TypeText());

        }
        if (Input.GetButtonDown("Submit") && flag == true) 
        {
            correntMessage++;
            if (Input.GetButtonDown("Submit") && flag == true && correntMessage == Messages.Count)
            {
                background.enabled = false;
                black.enabled = false;
                XboxKey.enabled = false;
                E.enabled = false;
                ui.enabled = true;
                image.enabled = false;
                textField.enabled = false;
                Time.timeScale = 1;
            }else 
            if (correntMessage <= Messages.Count)
            {
                StopCoroutine(coroutine1);
                textField.text = "";
                message = Messages[correntMessage];
                coroutine1 = StartCoroutine(TypeText());


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

