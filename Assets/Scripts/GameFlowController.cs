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

    [SerializeField]
    private SceneReference GameEnd;

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

    public void LoadGameEnd()
    {
        SceneManager.LoadScene(GameEnd.SceneName);
    }

    public void LoadShop()
    {
        SceneManager.LoadScene(Shop.SceneName);
    }
}
