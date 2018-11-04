using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultySetting : MonoBehaviour
{

    public GameObject MainMenu;

    public TMPro.TextMeshProUGUI TutIndicator;

    public void Update()
    {
        if (PlayerPrefs.HasKey("isTutorialEnabled"))
        {
            if (PlayerPrefs.GetInt("isTutorialEnabled") == 1) TutIndicator.text = "disable tutorial";
            else TutIndicator.text = "enable tutorial";
        }
        else TutIndicator.text = "disable tutorial";
    }

    public void SetEasy()
    {
        PlayerPrefs.SetFloat("difficultyKey", 0.3f);
    }

    public void SetHard()
    {
        PlayerPrefs.SetFloat("difficultyKey", 0.8f);
    }

    public void ReturnToMM()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void ToggleTutorial()
    {
        if (PlayerPrefs.HasKey("isTutorialEnabled"))
        {
            PlayerPrefs.SetInt("isTutorialEnabled", (1 - PlayerPrefs.GetInt("isTutorialEnabled")));
        }
        else PlayerPrefs.SetInt("isTutorialEnabled", 0);
        //int xd = PlayerPrefs.GetInt("isTutorialEnabled");
        //Debug.Log(xd);
    }



}
