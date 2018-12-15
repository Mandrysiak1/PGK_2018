using Game.Initialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSettings : MonoBehaviour
{

    [SerializeField]
    private LevelFlow LevelFlow;

    public GameObject MainMenu;

    private GameLevel Level1;
    private GameLevel Level2;
    private GameLevel Level3;
    private GameLevel Level4;

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
    private void Awake()
    {
        Level1 = LevelFlow.GetFirstLevel();
        Level2 = LevelFlow.GetNextLevel(Level1);
        Level3 = LevelFlow.GetNextLevel(Level2);
        Level4 = LevelFlow.GetNextLevel(Level3);
        ResetFirstButton();
    }

    public void Load1()
    {
        LevelLoader.StartLevel(Level1);
    }

    public void Load2()
    {
        LevelLoader.StartLevel(Level2);
    }

    public void Load3()
    {
        LevelLoader.StartLevel(Level3);
    }

    public void Load4()
    {
        LevelLoader.StartLevel(Level4);
    }

    public void ReturnToMM()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
        MainMenu.GetComponent<MainMenu>().ResetFirstButton();
    }
}
