using UnityEngine;

public class Cheats : MonoBehaviour
{
    public KeyCode AddWinstreakKey = KeyCode.P;
    [SerializeField]
    private MonoWinStreakSource WinStreak;

    private void Update()
    {
        if (Input.GetKeyDown(AddWinstreakKey))
        {
            WinStreak.WinStreak++;
        }
    }
}
