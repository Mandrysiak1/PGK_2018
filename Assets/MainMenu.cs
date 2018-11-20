using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    [SerializeField]
    private GameFlowController Flow;

    public GameObject DiffMenu;
    public TutorialScript Tutorial;
    public GameObject Level;

    public void PlayGame()
    {
        if (PlayerPrefs.HasKey("isTutorialEnabled") && PlayerPrefs.GetInt("isTutorialEnabled") == 0)
        {
            StartFirstLevel();
        }
        else
        {
            gameObject.SetActive(false);
            PlayerPrefs.SetInt("isTutorialEnabled", 0);
            SceneManager.LoadScene("Tutorialv2");
        }
    }

    private void StartFirstLevel()
    {
        Flow.StartFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ChangeToDifficultyMenu()
    {
        gameObject.SetActive(false);
        DiffMenu.SetActive(true);

    }

    public void ChangeLevel()
    {
        gameObject.SetActive(false);
        Level.SetActive(true);
    }
}
