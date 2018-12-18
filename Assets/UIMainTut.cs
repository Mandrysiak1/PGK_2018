using Assets.PGKScripts.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ale Spaghetti

public class UIMainTut : MonoBehaviour
{

    public class MenuActivation : UnityEvent<bool> { }


    public MainScript mainScript;
    public Canvas MainCanvas;
    public Text howManyBeers;
    public Slider bigBar;
    public Text EndGameText;
    public Canvas EndGameCanvas;
    public Button Restart;
    public Button MainMenu;
    public Canvas NextLvlCanv;
    public Canvas PauseCanvas;
    public Canvas EndGameMenuCanvas;
    public Canvas FailureCanvas;
    public Canvas SuccesCanvas;
    public GameObject MenuConatiner;

    public MenuActivation menuActiveStatusEvent = new MenuActivation();

    public List<Button> buttonsSelectableList = new List<Button>();

    bool _menuActivated;
    bool _inShop = false;
    public bool MenuActivated
    {
        get
        {
            return _menuActivated;
        }
        set
        {
            this._menuActivated = value;
            menuActiveStatusEvent.Invoke(value);
        }
    }
    int x = 2;
    float y = 4;

    internal void BackFromShop()
    {
        if (_inShop) _inShop = false;
    }

    // Use this for initialization
    void Start()
    {
        menuActiveStatusEvent.AddListener(MenuActivationListener);
        MenuActivated = false;
        FailureCanvas.enabled = false;
        SuccesCanvas.enabled = false;

        PauseCanvas.enabled = false;

        Time.timeScale = 1;
        EndGameCanvas.enabled = false;
        if (mainScript == null)
            mainScript = (MainScript)FindObjectOfType(typeof(MainScript));
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
        //Continue.onClick.AddListener(ContinueGame);
        SetButtons(true);

    }

    private void SetButtons(bool init)
    {
            EventSystem.current.SetSelectedGameObject(Restart.gameObject);
    }

    private void MenuActivationListener(bool arg0)
    {
        switch (arg0)
        {
            case true:
                this.MenuConatiner.SetActive(true);
                break;
            case false:
                this.MenuConatiner.SetActive(false);
                break;
        }
    }

    private void Awake()
    {
        //MenuConatiner.SetActive(true);
    }


    private void GameStateChanged(GameState arg0, GameState arg1)
    {
        if (arg1 == GameState.Success || arg1 == GameState.Failure)
        {
            MenuActivated = true;
            EventSystem.current.SetSelectedGameObject(Restart.gameObject);
            EndGameText.text = "you " + (arg1 == GameState.Success ? "win" : "lose");
            mainScript.ResetBeersHandedOut();

            EndGameCanvas.enabled = true;
            //if (SceneManager.GetActiveScene().buildIndex == 1)
            //{
            if (arg1 == GameState.Success)
            {
                NextLvlCanv.enabled = true;
                SuccesCanvas.enabled = true;
            }
            else if (arg1 == GameState.Failure)
            {
                FailureCanvas.enabled = true;
            }
            else
            {
                NextLvlCanv.enabled = false;
                FailureCanvas.enabled = false;
                SuccesCanvas.enabled = false;
            }
            // }
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!_inShop)
        {
            if (!IsInList(EventSystem.current.currentSelectedGameObject))
            {
                EventSystem.current.SetSelectedGameObject(Restart.gameObject);
            }
            if (mainScript.CurrentGameState == GameState.Paused
                    && !IsInPause(EventSystem.current.currentSelectedGameObject))
                EventSystem.current.SetSelectedGameObject(Restart.gameObject);
        }


        if (Input.GetButtonDown("PauseButton")) //CHANGE FOR PAD
        {
            if (mainScript.CurrentGameState == GameState.Playing)
            {
                MenuActivated = true;
                EndGameMenuCanvas.enabled = false;
                EndGameCanvas.enabled = true;
                EndGameCanvas.GetComponent<Image>().enabled = false;
                PauseCanvas.enabled = true;
                //Continue.gameObject.SetActive(false);
                Time.timeScale = 0;

                mainScript.CurrentGameState = GameState.Paused;
            }
            else if (mainScript.CurrentGameState == GameState.Paused)
            {
                MenuActivated = false;
                PauseCanvas.enabled = false;
                EndGameMenuCanvas.enabled = true;
                EndGameCanvas.enabled = false;
                EndGameCanvas.GetComponent<Image>().enabled = true;
                //Continue.gameObject.SetActive(true);
                Time.timeScale = 1;

                mainScript.CurrentGameState = GameState.Playing;
            }
        }

        if (UpgradeClass.exited)
        {
            EndGameCanvas.enabled = true;
            if (UpgradeClass.nextlvlcanvas) NextLvlCanv.enabled = true;
            UpgradeClass.exited = false;
        }
    }

    void RestartTheGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("BestTutorial");
    }

    void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;

    }
    
    private bool IsInList(GameObject go)
    {
        foreach (var el in buttonsSelectableList)
        {
            if (el.gameObject.Equals(go))
                return true;
        }
        return false;
    }
    private bool IsInPause(GameObject go)
    {
        return go.Equals(Restart.gameObject) || go.Equals(MainMenu.gameObject);
    }
}
