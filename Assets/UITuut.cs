using Assets.PGKScripts.Enums;
using System;
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ale Spaghetti

public class UITuut : MonoBehaviour
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
    public TextMeshProUGUI timer;
    public Canvas PauseCanvas;
    public Canvas GuestArrived;
    public Canvas witchUI;
    public Button gotonextlvl;
    bool gamePaused = false;
    int x = 2;
    float y = 4;

    // Use this for initialization
    void Start()
    {
        witchUI.enabled = false;
        PauseCanvas.enabled = false;
        GuestArrived.enabled = false;

        Time.timeScale = 1;
        EndGameCanvas.enabled = false;
        if (mainScript == null)
            mainScript = (MainScript)FindObjectOfType(typeof(MainScript));
        mainScript.DissatisfactionChanged.AddListener(DissatisfactionChanged);
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
        gotonextlvl.onClick.AddListener(NextLvlPls);


    }

    private void GameStateChanged(GameState arg0, GameState arg1)
    {
        EndGameText.text = "you " + (mainScript.CurrentGameState == GameState.Success ? "win" : "lose")
                        + ". your score: " + mainScript.Score;

        mainScript.ResetScore();
        Time.timeScale = 0;

        EndGameCanvas.enabled = true;

    }

    private void DissatisfactionChanged(float arg0, float arg1)
    {
        this.bigBar.value = mainScript.DissatisfactionValue;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) && mainScript.CurrentGameState == GameState.Playing)
        {
            if (!gamePaused)
            {
                gamePaused = true;
                EndGameCanvas.enabled = true;
                EndGameCanvas.GetComponent<Image>().enabled = false;
                PauseCanvas.enabled = true;
                Time.timeScale = 0;
            }
            else
            {
                gamePaused = false;
                PauseCanvas.enabled = false;
                EndGameCanvas.enabled = false;
                EndGameCanvas.GetComponent<Image>().enabled = true;
                Time.timeScale = 1;
            }
        }
        if (y >= 10)
            timer.text = x + ":" + (int)y;
        else timer.text = x + ":0" + (int)y;
        if (y < 0)
        {
            x -= 1;
            y = 60;
        }
        y -= Time.deltaTime;
    }

    void RestartTheGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorialv2");
        UpgradeClass.Tip = 0;
    }

    void ExitToMainMenu()
    {
        Time.timeScale = 1;
        Flow.LoadMainMenu();
        UpgradeClass.Tip = 0;
    }

    void LoadNextLvl()
    {
        Time.timeScale = 1;
        Flow.StartFirstLevel();
    }



    public void NextLvlPls()
    {
        Time.timeScale = 1;
        Flow.StartFirstLevel();
    }

}
