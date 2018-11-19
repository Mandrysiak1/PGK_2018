using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private MainScript Main;

    public GameLevel Level;

    private AsyncOperation Loading;

    // Ugly ugly ugly, but we somehow have to store it
    // between scenes
    private static GameLevel CurrentLevel;

    public static void StartTutorialLevel(GameLevel level)
    {
        StartLevel(level, "Tutorial");
    }

    public static void StartLevel(GameLevel level, string gameScene = "Game")
    {
        CurrentLevel = level;
        SceneManager.LoadScene(gameScene);
    }

    void Start()
    {
        if (CurrentLevel != null)
            Level = CurrentLevel;
        Loading = SceneManager.LoadSceneAsync(Level.Scene.SceneName, LoadSceneMode.Additive);
        Loading.completed += op => SetupScene(op, Level.Scene);
    }

    private void SetupScene(AsyncOperation op, SceneReference scene)
    {
        LevelScene levelScene = FindObjectOfType<LevelScene>();
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.SceneName));

        Main.Setup(Level, levelScene);
    }
}