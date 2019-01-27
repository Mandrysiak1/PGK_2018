using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameFlowController Flow;

    public void PlayGame()
    {
        StartFirstLevel();
    }

    private void StartFirstLevel()
    {
        Flow.StartFirstLevel();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
