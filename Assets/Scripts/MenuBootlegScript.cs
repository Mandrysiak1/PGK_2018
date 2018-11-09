using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuBootlegScript : MonoBehaviour {

    public Button m_YourFirstButton, m_YourSecondButton;

    private void Start()
    {
        m_YourFirstButton.onClick.AddListener(StartTheGame);
        m_YourSecondButton.onClick.AddListener(QuitTheGame);
    }

    void StartTheGame()
    {
        SceneManager.LoadScene(1);        
    }

    void QuitTheGame()
    {
        Application.Quit();
    }
}
