using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour {

    public GameObject MainMenu;

    public void Load1()
    {
        SceneManager.LoadScene(1);
    }

    public void Load2()
    {
        SceneManager.LoadScene(2);
    }

    public void ReturnToMM()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
