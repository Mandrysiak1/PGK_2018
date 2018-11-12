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

    public static void StartLevel(GameLevel level)
    {
        CurrentLevel = level;
        SceneManager.LoadScene("Game");
    }

    void Start()
    {
        if(CurrentLevel != null)
            Level = CurrentLevel;
        Loading = SceneManager.LoadSceneAsync(Level.Scene.SceneName, LoadSceneMode.Additive);
        Loading.completed += SetupScene;
    }

    private void SetupScene(AsyncOperation obj)
    {
        LevelScene levelScene = FindObjectOfType<LevelScene>();
        Main.Setup(Level, levelScene);
    }
}
