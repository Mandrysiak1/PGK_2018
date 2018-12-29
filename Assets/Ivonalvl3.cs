using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Ivonalvl3 : MonoBehaviour
{
    float time = 0f;
    public Image background;
    public Image XboxKey;
    public Text bullshitspewingoutofcharactersmouthholyshitbarbecue;
    public Image charactersdirtyassfacewhatthehellwhyarethesevariablenamessolong;
    string message;
    public TextMeshProUGUI E;
    public float letterPaused = 0.1f;
    bool[] flags = new bool[99];
    // Use this for initialization
    void Start()
    {
        E = XboxKey.GetComponentInChildren<TextMeshProUGUI>();
        background.enabled = false;
        XboxKey.enabled = false;
        E.enabled = true;
        charactersdirtyassfacewhatthehellwhyarethesevariablenamessolong.enabled = false;
        bullshitspewingoutofcharactersmouthholyshitbarbecue.enabled = false;

    }

    void Update()
    {

        if (waiths() && flags[0] == false)
        {
            Time.timeScale = 0;
            flags[0] = true;
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
        if (Input.GetButtonDown("Submit") && flags[0] == true)
        {
            if (message == bullshitspewingoutofcharactersmouthholyshitbarbecue.text && flags[1] == false)
            {
                //StopCoroutine(TypeText());
                bullshitspewingoutofcharactersmouthholyshitbarbecue.text = "";
                message = "My elves are busy making new toys... They are hungry and exhausted... It's hopeless! We have to call off the Christmas Eve!";

                flags[1] = true;
                StartCoroutine(TypeText());
                //Time.timeScale = 1;
            }
            if (message == bullshitspewingoutofcharactersmouthholyshitbarbecue.text && flags[1] == true && flags[2] == false) 
            {
                //StopCoroutine(TypeText());
                bullshitspewingoutofcharactersmouthholyshitbarbecue.text = "";
                message = "Or... maybe you can help us? Please, take care of my little helpers and bring me the gifts that I am asking for, can you? We cannot disappoint the children!";

                flags[2] = true;
                StartCoroutine(TypeText());
                //Time.timeScale = 1;
            }
            if (message == bullshitspewingoutofcharactersmouthholyshitbarbecue.text && flags[2] == true)
            {
                //StopCoroutine(TypeText());
                background.enabled = false;
                XboxKey.enabled = false;
                E.enabled = false;
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

