using System;
using QTE;
using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    [Serializable]
    public class OnPausedEvent : UnityEvent<bool> {}

    public bool Paused { get; private set; }
    public OnPausedEvent OnPaused;
    public ButtonInput Input;

    private float _OldTimeScale;

    private void Start()
    {
        Input.Subscribe(Toggle);
    }

    private void OnDestroy()
    {
        if (Paused)
            Toggle();
    }

    public void Toggle()
    {
        if (!enabled)
            return;

        if (!Paused)
        {
            _OldTimeScale = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = _OldTimeScale;
        }
        Paused = !Paused;
        OnPaused.Invoke(Paused);
    }
}
