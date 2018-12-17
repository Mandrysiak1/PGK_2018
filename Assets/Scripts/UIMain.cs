using Assets.PGKScripts.Enums;
using System;
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ale Spaghetti

public class UIMain : MonoBehaviour
{

    public class MenuActivation : UnityEvent<bool> { }

    [SerializeField]
    private GameFlowController Flow;

    public MainScript mainScript;
    public Canvas MainCanvas;
    public Text howManyBeers;
    public Slider bigBar;
    public Text EndGameText;
    public Canvas EndGameCanvas;
    public Button Restart;
    public Button MainMenu;
    public Canvas NextLvlCanv;
    public Button NextLevel;
    public Button GoToShop;
    public Button Continue;
    public Canvas PauseCanvas;
    public Canvas EndGameMenuCanvas;
    public Canvas FailureCanvas;
    public Canvas SuccesCanvas;
    public GameObject MenuConatiner;
    public ScoreSystem scoreSystem;
    public OrderGenerator orderGenerator;
    
    public MenuActivation menuActiveStatusEvent = new MenuActivation();
    bool _menuActivated;
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
            mainScript = (MainScript) FindObjectOfType(typeof(MainScript));
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
        //Continue.onClick.AddListener(ContinueGame);
        SetButtons();

    }

    private void SetButtons()
    {
        if (Flow.HasNextLevel())
        {
            NextLevel.onClick.AddListener(LoadNextLvl);
            NextLevel.gameObject.SetActive(true);
            GoToShop.onClick.AddListener(LoadShop);
            GoToShop.gameObject.SetActive(true);
        }
        else
        {
            NextLevel.gameObject.SetActive(false);
            GoToShop.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(Restart.gameObject);
        }
    }

    private void MenuActivationListener(bool arg0)
    {
        switch(arg0)
        {
            case true:
                this.MenuConatiner.SetActive(true);
                orderGenerator.enabled = false;
                break;
            case false:
                this.MenuConatiner.SetActive(false);
                orderGenerator.enabled = true;
                break;
        }
    }

    private void Awake()
    {
        //MenuConatiner.SetActive(true);
    }


    private void GameStateChanged(GameState arg0, GameState arg1)
    {
        if(arg1 == GameState.Success || arg1 == GameState.Failure)
        {
            MenuActivated = true;
            EndGameText.text = "you " + (arg1 == GameState.Success ? "win" : "lose")
                                    + ". your score: " + scoreSystem.Score;
        
            scoreSystem.ResetScore();
            mainScript.ResetBeersHandedOut();

            EndGameCanvas.enabled = true;
            //if (SceneManager.GetActiveScene().buildIndex == 1)
            //{
                if (arg1 == GameState.Success)
                {
                    NextLvlCanv.enabled = true;
                    SuccesCanvas.enabled = true;
                }
                else if(arg1 == GameState.Failure)
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
            else if(mainScript.CurrentGameState == GameState.Paused)
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
        Flow.RestartCurrentLevel();
    }

    void ExitToMainMenu()
    {
        Time.timeScale = 1;
        Flow.LoadMainMenu();
    }

    void LoadNextLvl()
    {
        Time.timeScale = 1;
        Flow.StartNextLevel();
        mainScript.CurrentGameState = GameState.Playing;
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;

    }
    void LoadShop()
    {
        UpgradeClass.endgamecanvas = EndGameCanvas.enabled;
        UpgradeClass.nextlvlcanvas = NextLvlCanv.enabled;
        //EndGameCanvas.gameObject.SetActive(false);
        NextLvlCanv.enabled = false;
        MainCanvas.enabled = false;
        GameObject Customers = GameObject.Find("Customers");
        if (Customers != null)
            Customers.SetActive(false);
        GameObject Obstacles = GameObject.Find("Obstacles");

        if (Obstacles != null)
        {
            Obstacles.SetActive(false);
            Obstacles.GetComponent<ObstacleGenerator>().CancelInvoke();
        }
        MenuActivated = false;

        Time.timeScale = 1;
        
        SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
    }

}
