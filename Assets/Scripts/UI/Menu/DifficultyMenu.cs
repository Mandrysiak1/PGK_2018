using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyMenu : MonoBehaviour
{
    public GameSettings Settings;

    public void SetEasy()
    {
        Settings.Difficulty = GameDifficulty.Easy;
    }

    public void SetHard()
    {
        Settings.Difficulty = GameDifficulty.Hard;
    }

    public void RunTutorial()
    {
        SceneManager.LoadScene("BestTutorial");
    }



}
