using Assets.PGKScripts.Enums;
using UnityEngine;

public class WinGameWhenTimeIsOver : MonoBehaviour
{
    public float Limit = 124;

    public float Timer { get; set; }

    [SerializeField]
    private TimerUI UI;
    [SerializeField]
    private MainScript Main;

    private void Start()
    {
        Timer = 0.0f;
    }

    public void Reset()
    {
        Timer = 0.0f;
    }

    private void Update()
    {
        Timer += Time.deltaTime;

        float timeLeft = Mathf.Max(0, Limit - Timer);
        UI.Value = timeLeft;

        if (Timer > Limit)
        {
            if (Main.CurrentGameState != GameState.Success && Main.CurrentGameState != GameState.Failure)
            {
                Main.GameOver(GameState.Success);
            }
        }
    }
}
