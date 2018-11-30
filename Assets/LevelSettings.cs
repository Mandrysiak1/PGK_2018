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

    public void ReturnToMM()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
        MainMenu.GetComponent<MainMenu>().ResetFirstButton();
    }
}
