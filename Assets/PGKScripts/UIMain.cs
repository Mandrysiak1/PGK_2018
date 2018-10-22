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
    int x = 2;
    float y = 4;

    // Use this for initialization
    void Start () {
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
            EndGameText.text = "You " + (mainScript.CurrentGameState == GameState.Success? "win" : "lose") 
                + ". Your score: " + mainScript.Score;
            Time.timeScale = 0;
            EndGameCanvas.enabled = true;
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
