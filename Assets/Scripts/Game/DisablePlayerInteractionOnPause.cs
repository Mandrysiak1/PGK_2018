using UnityEngine;

public class DisablePlayerInteractionOnPause : MonoBehaviour
{
    [SerializeField]
    private PauseController Pause;

    [SerializeField]
    private Player Player;

    private void Start()
    {
        Pause.OnPaused.AddListener(OnPaused);
    }

    private void OnPaused(bool pause)
    {
        Player.Interactive = !pause;
    }
}
