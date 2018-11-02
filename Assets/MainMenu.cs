using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour {

    public GameObject DiffMenu;
    public GameObject Tutorial;

    public void PlayGame()
    {
        if(PlayerPrefs.HasKey("isTutorialEnabled") && PlayerPrefs.GetInt("isTutorialEnabled")==0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
        {
            gameObject.SetActive(false);
            Tutorial.SetActive(true);
        }
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
}
