using Assets.PGKScripts.Enums;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMain : MonoBehaviour {

    MainScript mainScript;
    public Text howManyBeers;
    public Slider bigBar;
    public Text EndGameText;
    public Canvas EndGameCanvas;
    public Button Restart;
    public Button MainMenu;
    public Text timer;
    public Canvas PauseCanvas;
    bool gamePaused = false;
    int x = 2;
    float y = 4;

    public AudioSource backgroundSong;

    // Use this for initialization
    void Start () {

        this.backgroundSong = GetComponent<AudioSource>();
        this.backgroundSong.Play(0);
        PauseCanvas.enabled = false;

        Time.timeScale = 1;
        EndGameCanvas.enabled = false;
        mainScript = (MainScript)FindObjectOfType(typeof(MainScript));
        mainScript.PropertyChanged += MainScript_PropertyChanged;
        Restart.onClick.AddListener(RestartTheGame);
        MainMenu.onClick.AddListener(ExitToMainMenu);
    }

    private void MainScript_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName.Equals("BeerCount"))
            this.howManyBeers.text = mainScript.BeerCount + " x";
        if (e.PropertyName.Equals("DissatisfactionValue"))
            this.bigBar.value = mainScript.DissatisfactionValue;
        if (e.PropertyName.Equals("CurrentGameState"))
        {
            EndGameText.text = "you " + (mainScript.CurrentGameState == GameState.Success? "win" : "lose") 
                + ". your score: " + mainScript.Score;
            mainScript.ResetScore();
            Time.timeScale = 0;
            EndGameCanvas.enabled = true;
        }

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

    }

    // Update is called once per frame
    void Update () {
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
        SceneManager.LoadScene(1);
    }

    void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    
}
