using Assets.PGKScripts.Enums;
using System;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    [SerializeField]
    private GameFlowController Flow;

    public MainScript mainScript;
    public Text howManyBeers;
    public Slider bigBar;
    public Text EndGameText;
    public Canvas EndGameCanvas;
    public Button Restart;
    public Button MainMenu;
    public Canvas NextLvlCanv;
    public Button NextLevel;
    public Text timer;
    public Canvas PauseCanvas;
    bool gamePaused = false;
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
        mainScript.DissatisfactionChanged.AddListener(DissatisfactionChanged);
        mainScript.GameStatusChanged.AddListener(GameStateChanged);
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
        if (Flow.HasNextLevel())
        {
            NextLevel.onClick.AddListener(LoadNextLvl);
            NextLevel.gameObject.SetActive(true);
        }
        else
        {
            NextLevel.gameObject.SetActive(false);
        }
    }

    private void GameStateChanged(GameState arg0, GameState arg1)
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

    private void DissatisfactionChanged(float arg0, float arg1)
    {
        this.bigBar.value = mainScript.DissatisfactionValue;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

}
