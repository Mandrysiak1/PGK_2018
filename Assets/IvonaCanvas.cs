using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IvonaCanvas : MonoBehaviour {
    float time = 0f;
    public Image background;
    public Image XboxKey;
    public Text bullshitspewingoutofcharactersmouthholyshitbarbecue;
    public Image charactersdirtyassfacewhatthehellwhyarethesevariablenamessolong;
    string message;
    public float letterPaused = 0.1f;
    bool flag;
    // Use this for initialization
    void Start()
    {
        background.enabled = false;
        XboxKey.enabled = false;
        charactersdirtyassfacewhatthehellwhyarethesevariablenamessolong.enabled = false;
        bullshitspewingoutofcharactersmouthholyshitbarbecue.enabled = false;

    }

    void Update()
    {

        if (waiths() && flag == false)
        {
            Time.timeScale = 0;
            flag = true;
            //Message will display will be at Text
            message = bullshitspewingoutofcharactersmouthholyshitbarbecue.text;
            //Set the text to be blank first
            bullshitspewingoutofcharactersmouthholyshitbarbecue.text = "";
            background.enabled = true;
            XboxKey.enabled = true;
            charactersdirtyassfacewhatthehellwhyarethesevariablenamessolong.enabled = true;
            bullshitspewingoutofcharactersmouthholyshitbarbecue.enabled = true;
            //Call the function and expect yield to return
            StartCoroutine(TypeText());
        }
        if (Input.GetButtonDown("Submit") && flag == true) 
        {
            if(message == bullshitspewingoutofcharactersmouthholyshitbarbecue.text)
            {
                //StopCoroutine(TypeText());
                background.enabled = false;
                XboxKey.enabled = false;
                charactersdirtyassfacewhatthehellwhyarethesevariablenamessolong.enabled = false;
                bullshitspewingoutofcharactersmouthholyshitbarbecue.enabled = false;
                Time.timeScale = 1;
            }
        }
    }

    IEnumerator TypeText()
    {

        foreach (char letter in message.ToCharArray())
        {
            //Add 1 letter each
            bullshitspewingoutofcharactersmouthholyshitbarbecue.text += letter;
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

