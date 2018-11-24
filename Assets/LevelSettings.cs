using System.Collections;
using System.Collections.Generic;
using Game.Initialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSettings : MonoBehaviour
{

    [SerializeField]
    private LevelFlow LevelFlow;

    public GameObject MainMenu;

    private GameLevel Level1;
    private GameLevel Level2;

    private void Awake()
    {
        Level1 = LevelFlow.GetFirstLevel();
        Level2 = LevelFlow.GetNextLevel(Level1);
    }

    public void Load1()
    {
        LevelLoader.StartLevel(Level1);
    }

    public void Load2()
    {
        LevelLoader.StartLevel(Level2);
    }

    public void ReturnToMM()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
    }
}
