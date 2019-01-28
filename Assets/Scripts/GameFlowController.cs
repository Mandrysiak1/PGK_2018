using Game.Initialization;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName = "Beerfest/Game Flow Controller")]
public class GameFlowController : ScriptableObject
{
    public LevelFlow Flow;

    [SerializeField]
    private SceneReference MainMenu;

    [SerializeField]
    private SceneReference Shop;

    private GameLevel CurrentLevel;

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenu.SceneName);
    }

    public void StartFirstLevel()
    {
        CurrentLevel = Flow.GetFirstLevel();
        StartCurrentLevel();
    }

    public void StartNextLevel()
    {
        CurrentLevel = Flow.GetNextLevel(CurrentLevel);
        StartCurrentLevel();
    }

    public void StartLevel(GameLevel level)
    {
        CurrentLevel = level;
        StartCurrentLevel();
    }

    public void RestartCurrentLevel()
    {
        StartCurrentLevel();
    }

    private void StartCurrentLevel()
    {
        LevelLoader.StartLevel(CurrentLevel);
    }

    public bool HasNextLevel()
    {
        return Flow.GetNextLevel(CurrentLevel) != null;
    }
    public bool isLevel4()
    {
        if (Flow.GetNextLevel(Flow.GetNextLevel(CurrentLevel)) == null)
            return true;
        else
            return false;
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(Shop.SceneName);
    }
}
