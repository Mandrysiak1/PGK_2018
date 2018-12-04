using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultySetting : MonoBehaviour
{

    public GameObject MainMenu;

    public TMPro.TextMeshProUGUI TutIndicator;

    public GameObject standardSetObject;
    public void ResetFirstButton()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(standardSetObject);
    }
    public void Start()
    {
        ResetFirstButton();
    }
    public void Awake()
    {
        ResetFirstButton();
    }
    public void Update()
    {
        /*if (PlayerPrefs.HasKey("isTutorialEnabled"))
        {
            if (PlayerPrefs.GetInt("isTutorialEnabled") == 1) TutIndicator.text = "disable tutorial";
            else TutIndicator.text = "enable tutorial";
        }
        else TutIndicator.text = "disable tutorial";*/
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
        MainMenu.GetComponent<MainMenu>().ResetFirstButton();
    }

    public void ToggleTutorial()
    {
        /*if (PlayerPrefs.HasKey("isTutorialEnabled"))
        {
            PlayerPrefs.SetInt("isTutorialEnabled", (1 - PlayerPrefs.GetInt("isTutorialEnabled")));
        }
        else PlayerPrefs.SetInt("isTutorialEnabled", 0);
        //int xd = PlayerPrefs.GetInt("isTutorialEnabled");
        //Debug.Log(xd);*/
        SceneManager.LoadScene("BestTutorial");
    }



}
