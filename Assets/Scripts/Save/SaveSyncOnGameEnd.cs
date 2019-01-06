using Assets.PGKScripts.Enums;
using UnityEngine;

public class SaveSyncOnGameEnd : MonoBehaviour
{
    [SerializeField]
    private MainScript Main;

    [SerializeField]
    private SaveSystem Save;

    private void Start()
    {
        if (!Main.tutorial)
        {
            Main.GameStatusChanged.AddListener(GameStateChanged);
        }
    }

    private void GameStateChanged(GameState previous, GameState current)
    {
        if (previous == GameState.Playing)
        {
            if (current == GameState.Failure || current == GameState.Success)
            {
                Debug.Log("Game ended, syncing save!");
                Save.Sync();
            }
        }
    }
}
