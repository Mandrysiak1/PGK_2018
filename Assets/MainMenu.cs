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
        StartFirstLevel();
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
