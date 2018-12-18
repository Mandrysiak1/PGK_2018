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

public class UIMainTut : MonoBehaviour
{
    [SerializeField]

    public MainScript mainScript;
    public Canvas MainCanvas;
    public Text howManyBeers;
    public Slider bigBar;
    public Text EndGameText;
    public Canvas EndGameCanvas;
    public Button Restart;
    public Button MainMenu;
    public GameFlowController flow;
    public Button Continue;
    public Canvas PauseCanvas;
    public Canvas SuccessCanvas;
    public ScoreSystem scoreSystem;
    int x = 2;
    float y = 4;

    // Use this for initialization
    void Start()
    {
        PauseCanvas.enabled = false;

        Time.timeScale = 1;
        EndGameCanvas.enabled = false;
        if (mainScript == null)
            mainScript = (MainScript)FindObjectOfType(typeof(MainScript));
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
       

    }



    private void GameStateChanged(GameState arg0, GameState arg1)
    {
        if (arg1 == GameState.Success || arg1 == GameState.Failure)
        {
            EndGameText.text = "you " + (mainScript.CurrentGameState == GameState.Success ? "win" : "lose");
            scoreSystem.ResetScore();
            mainScript.ResetBeersHandedOut();
            Time.timeScale = 0;

            EndGameCanvas.enabled = true;

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
            else if (mainScript.CurrentGameState == GameState.Paused)
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
        flow.LoadMainMenu();
    }


    private void ContinueGame()
    {
        Time.timeScale = 1;

    }
    

}
