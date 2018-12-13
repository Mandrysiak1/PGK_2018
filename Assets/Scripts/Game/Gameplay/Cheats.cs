using UnityEngine;

public class Cheats : MonoBehaviour
{
    public KeyCode AddWinstreakKey = KeyCode.P;
    public KeyCode DecreaseTime = KeyCode.O;
    public KeyCode AddTip = KeyCode.L;
    public int TimeDecrease = 5;

    [SerializeField]
    private MonoWinStreakSource WinStreak;
    [SerializeField]
    private WinGameWhenTimeIsOver Time;

    private void Update()
    {
        if (Input.GetKeyDown(AddWinstreakKey))
        {
            WinStreak.WinStreak++;
        }

        if (Input.GetKeyDown(DecreaseTime))
        {
            //Time.Limit -= TimeDecrease;
            Time.Timer += TimeDecrease;
        }

        if (Input.GetKeyDown(AddTip))
        {
            UpgradeClass.Tip += 10;
        }
    }
}
