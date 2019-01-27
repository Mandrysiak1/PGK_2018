using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameFlowController Flow;

    public void PlayGame()
    {
        UpgradeClass.Reset();
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
