using UnityEngine;

public class GameSettings : ScriptableObject
{
    private readonly string GameDifficultyKey = "GameDifficulty";

    public GameDifficulty Difficulty
    {
        get
        {
            if (!Loaded)
                Load();
            return _Difficulty;
        }
        set
        {
            if (_Difficulty != value)
            {
                _Difficulty = value;
                Save();
            }
        }
    }

    [SerializeField]
    private GameDifficulty _Difficulty;
    private bool Loaded = false;

    private void OnEnable()
    {
        Loaded = false;
    }

    private void OnDisable()
    {
        Loaded = false;
    }

    private void Load()
    {
        Debug.Log("Settings loaded!");
        _Difficulty = (GameDifficulty)PlayerPrefs.GetInt(GameDifficultyKey);
        Loaded = true;
    }

    private void Save()
    {
        PlayerPrefs.SetInt(GameDifficultyKey, (int)_Difficulty);
    }
}
