using UnityEngine;

public class LoseWinstreakWhenDissatisfactionIncreased : MonoBehaviour
{
    [SerializeField]
    private MonoWinStreakSource WinStreakSource;
    [SerializeField]
    private MoodBasedDissatisfaction Dissatisfaction;

    private void Start()
    {
        Dissatisfaction.DissatisfactionChanged.AddListener(DissatisfactionChanged);
    }

    private void DissatisfactionChanged(float current, float old)
    {
        if (current > old)
        {
            WinStreakSource.WinStreak = 0;
        }
    }
}
