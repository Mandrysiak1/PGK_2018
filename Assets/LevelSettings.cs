using Game.Initialization;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelSettings : MonoBehaviour
{
    [SerializeField]
    private GameFlowController GameFlow;

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
        Level1 = GameFlow.Flow.GetFirstLevel();
        Level2 = GameFlow.Flow.GetNextLevel(Level1);
        Level3 = GameFlow.Flow.GetNextLevel(Level2);
        Level4 = GameFlow.Flow.GetNextLevel(Level3);
        ResetFirstButton();
    }

    public void Load1()
    {
        GameFlow.StartLevel(Level1);
    }

    public void Load2()
    {
        GameFlow.StartLevel(Level2);
    }

    public void Load3()
    {
        GameFlow.StartLevel(Level3);
    }

    public void Load4()
    {
        GameFlow.StartLevel(Level4);
    }

    public void ReturnToMM()
    {
        gameObject.SetActive(false);
        MainMenu.SetActive(true);
        MainMenu.GetComponent<MainMenu>().ResetFirstButton();
    }
}
