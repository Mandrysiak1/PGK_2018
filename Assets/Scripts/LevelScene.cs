using System;
using System.Linq;
using Assets.PGKScripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    [SerializeField]
    private GameFlowController Flow;

    public Transform PlayerStartingPosition;
    public Transform CameraStartingPosition;

    private void Start()
    {
        if (Flow != null)
        {
            GameContext context = null;
            GameContext.FindIfNull(ref context);
            if (context == null)
            {
                var currentScene = SceneManager.GetActiveScene().name;
                GameLevel thisLevel =
                    Flow.Flow.AllLevels.FirstOrDefault(level => level.Scene.SceneName == currentScene);
                if (thisLevel == null)
                {
                    Debug.LogError("No level associated with this scene!");
                    return;
                }

                Debug.Log("Starting level associated with this scene");
                Flow.StartLevel(thisLevel);
            }
        }
    }
}
