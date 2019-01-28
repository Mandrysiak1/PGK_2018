using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameFlowController Flow;
    [SerializeField]
    private PauseController Pause;
    [SerializeField]
    private MenuContent Content;

    private void Start()
    {
        Content.Disable();
        Pause.OnPaused.AddListener(OnPaused);
    }

    public void Quit()
    {
        Flow.LoadMainMenu();
    }

    public void Restart()
    {
        Flow.RestartCurrentLevel();
    }

    private void OnPaused(bool paused)
    {
        if(paused)
            Content.Enable();
        else
            Content.Disable();
    }
}
