using UnityEngine;

public class EnableBasedOnDifficulty : MonoBehaviour
{
    [SerializeField]
    private GameSettings Settings;
    [SerializeField]
    private GameObject Easy, Hard;

    private void Start()
    {
        switch(Settings.Difficulty)
        {
            case GameDifficulty.Easy:
                Easy.gameObject.SetActive(true);
                break;
            case GameDifficulty.Hard:
                Hard.gameObject.SetActive(true);
                break;
        }
    }
}
