using Assets.PGKScripts.Interfaces;
using Assets.PGKScripts.Perks.WinStreak;
using UnityEngine;

public class MonoWinStreakSource : MonoBehaviour, IWinStreakSource
{
    [SerializeField]
    private int _WinStreak = 0;

    public int WinStreak
    {
        get { return _WinStreak;}
        set
        {
            var old = _WinStreak;
            _WinStreak = value;
            if (old != value)
            {
                _WinStreakChanged.Invoke(old, value);
            }
        }
    }

    public WinStreakEvent WinStreakChanged
    {
        get { return _WinStreakChanged; }
    }

    private WinStreakEvent _WinStreakChanged = new WinStreakEvent();

}
