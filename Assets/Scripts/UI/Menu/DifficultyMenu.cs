using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyMenu : MonoBehaviour
{
    public GameSettings Settings;
    public string SelectionText = ">";

    [SerializeField]
    private TextMeshProUGUI EasySelection, HardSelection;

    private void Start()
    {
        if(Settings.Difficulty == GameDifficulty.Easy)
            SetEasy();
        else
            SetHard();
    }

    public void SetEasy()
    {
        Settings.Difficulty = GameDifficulty.Easy;
        EasySelection.text = SelectionText;
        HardSelection.text = "";
    }

    public void SetHard()
    {
        Settings.Difficulty = GameDifficulty.Hard;
        HardSelection.text = SelectionText;
        EasySelection.text = "";
    }

    public void RunTutorial()
    {
        SceneManager.LoadScene("BestTutorial");
    }



}
