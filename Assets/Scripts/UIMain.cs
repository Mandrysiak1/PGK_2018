using Assets.PGKScripts.Enums;
using System;
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ale Spaghetti

public class UIMain : MonoBehaviour
{
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
    public Canvas SuccessCanvas;
    int x = 2;
    float y = 4;

    // Use this for initialization
    void Start()
    {
        PauseCanvas.enabled = false;

        Time.timeScale = 1;
        EndGameCanvas.enabled = false;
        if (mainScript == null)
            mainScript = (MainScript) FindObjectOfType(typeof(MainScript));
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
        //Continue.onClick.AddListener(ContinueGame);
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
        }

    }



    private void GameStateChanged(GameState arg0, GameState arg1)
    {
        if(arg1 == GameState.Success || arg1 == GameState.Failure)
        {
            EndGameText.text = "you " + (mainScript.CurrentGameState == GameState.Success ? "win" : "lose")
                                    + ". your score: " + mainScript.Score;

            mainScript.ResetScore();
            Time.timeScale = 0;

            EndGameCanvas.enabled = true;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                if (mainScript.CurrentGameState == GameState.Success)
                {
                    NextLvlCanv.enabled = true;
                }
                else
                {
                    NextLvlCanv.enabled = false;
                }
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
       

        if (Input.GetButtonDown("PauseButton")) //CHANGE FOR PAD
        {
            if (mainScript.CurrentGameState == GameState.Playing)
            {
                SuccessCanvas.enabled = false;
                EndGameCanvas.enabled = true;
                EndGameCanvas.GetComponent<Image>().enabled = false;
                PauseCanvas.enabled = true;
                //Continue.gameObject.SetActive(false);
                Time.timeScale = 0;

                mainScript.CurrentGameState = GameState.Paused;
            }
            else if(mainScript.CurrentGameState == GameState.Paused)
            {
                PauseCanvas.enabled = false;
                SuccessCanvas.enabled = true;
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
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;

    }
    void LoadShop()
    {
        UpgradeClass.endgamecanvas = EndGameCanvas.enabled;
        UpgradeClass.nextlvlcanvas = NextLvlCanv.enabled;
        EndGameCanvas.enabled = false;
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

        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Shop", LoadSceneMode.Additive);
    }

}
