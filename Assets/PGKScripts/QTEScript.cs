using Assets.PGKScripts;
using Assets.PGKScripts.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class QTEScript : MonoBehaviour, INotifyPropertyChanged, IQteScript {

    private List<string> charList  = new List<string>();

    private System.Random randomNum = new System.Random();

    private float time = 0;

    private Player myPlayer;

    private string randomChar;

    bool isWaitingForKey;

    float qteEndTime;

    

    public event PropertyChangedEventHandler PropertyChanged;

    private string _currentChar = "0";
    public string CurrentChar
    {
        get
        {
            return _currentChar;
        }
        private set
        {
            _currentChar = value;
            OnPropertyChanged("CurrentChar");
        }
    }

    private bool _success = false;
    public bool Success
    {
        get
        {
            return _success;
        }
        private set
        {
            _success = value;
            OnPropertyChanged("Success");
        }
    }

    void Start () {
        charList.Add("q");
        charList.Add("e");
        charList.Add("x");
        charList.Add("c");

        var x = FindObjectOfType(typeof(MainScript));
        time = ((MainScript)x).GetTime();
        myPlayer = ((MainScript)x).GetPlayer();
    }
	private void ResetUI()
    {
        CurrentChar = "0";
    }

	void Update ()
    {
        var x = FindObjectOfType(typeof(MainScript));
        time = ((MainScript)x).GetTime();

        if (isWaitingForKey)
        {
            if (time <= qteEndTime)
            {
                if (!(Input.GetKeyDown("w") || Input.GetKeyDown("a") 
                || Input.GetKeyDown("d") || Input.GetKeyDown("s")))
                {
                    if (Input.anyKeyDown)
                    {
                        if (Input.GetKeyDown(randomChar))
                        {
                            Success = true;
                            //_success = true;
                            isWaitingForKey = false;
                            Debug.Log("Dobrze");
                            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.8f);
                            ResetUI();

                        }
                        else
                        {
                            Success = false;
                            //_success = false;
                            Debug.Log("Źle");
                            myPlayer.SetBeersOnPlateQuantity(0);
                            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.8f);
                            isWaitingForKey = false;
                            ResetUI();
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Czas minął");
                myPlayer.SetBeersOnPlateQuantity(0);
                
                GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0.8f);
                isWaitingForKey = false;
                ResetUI();
            }

                
        }

 
    }
    /*
     * 
     * 
     * PREVIOUS VERSION
     * 
     * 
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
    */
    private void OnCollisionEnter(Collision collision)
    {
        var other = collision.gameObject;
        if (other.CompareTag("Player"))
        {

            GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonCharacter>().setm_MoveSpeedMultiplie(0);
            Debug.Log("QTE");
            isWaitingForKey = true;
            randomChar = charList[Random.Range(0, 3)];
            Debug.Log("Naciśnij: " + randomChar);
            //UIScript.SetImage(randomChar);
            CurrentChar = randomChar;
            qteEndTime = time + 2f;
        }
    }
    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
 
}
