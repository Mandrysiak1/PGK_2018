using Assets.PGKScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class QTE_Script : MonoBehaviour {

    private List<string> charList  = new List<string>();

    private System.Random randomNum = new System.Random();

    private float time = 0;

    private Player myPlayer;

    private string randomChar;

    bool isWaitingForKey;

    float qteEndTime;

    private QTE_UI_Script script;




    void Start () {
        charList.Add("q");
        charList.Add("e");
        charList.Add("x");
        charList.Add("c");

        var x = FindObjectOfType(typeof(MainScript));
        time = ((MainScript)x).GetTime();
        myPlayer = ((MainScript)x).GetPlayer();
        script = (QTE_UI_Script) FindObjectOfType(typeof(QTE_UI_Script));

    }
	

	void Update () {
        var x = FindObjectOfType(typeof(MainScript));
        time = ((MainScript)x).GetTime();

        for (int i = 0; i < 1; i++) //If something is silly but it works it is not silly anymore XD
        {

            if (isWaitingForKey == true)
            {
                if (time <= qteEndTime)
                {
                    if (Input.GetKeyDown("w") || Input.GetKeyDown("a") || Input.GetKeyDown("d") || Input.GetKeyDown("s"))
                    {
                        break;
                    }
                    if (Input.anyKeyDown)
                    {

                        if (Input.GetKeyDown(randomChar))
                        {
                            isWaitingForKey = false;
                            Debug.Log("Dobrze");
                            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.8f);
                            script.setImage("0");

                        }
                        else
                        {
                            Debug.Log("Źle");
                            myPlayer.setBOP(0);
                            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.8f);
                            isWaitingForKey = false;
                            script.setImage("0");
                        }

                    }

                } else
                {
                    Debug.Log("Czas minął");
                    myPlayer.setBOP(0);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.8f);
                    isWaitingForKey = false;
                    script.setImage("0");
                }

            }
        }

 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0);
            Debug.Log("QTE");
            isWaitingForKey = true;
            randomChar = charList[Random.Range(0, 3)];
            Debug.Log("Naciśnij: " + randomChar);
            script.setImage(randomChar);
            qteEndTime = time + 2f;
        }
    }
}
