using UnityEngine;

public class Cheats : MonoBehaviour
{
    public KeyCode AddWinstreakKey = KeyCode.P;
    public KeyCode DecreaseTime = KeyCode.O;
    public int TimeDecrease = 5;

    [SerializeField]
    private MonoWinStreakSource WinStreak;
    [SerializeField]
    private FailGameWhenTimeIsOver Time;

    private void Update()
    {
        if (Input.GetKeyDown(AddWinstreakKey))
        {
            WinStreak.WinStreak++;
        }

        if (Input.GetKeyDown(DecreaseTime))
        {
            Time.Limit -= TimeDecrease;
        }
    }
}
