using Assets.PGKScripts.Perks.WinStreak;
using System.ComponentModel;

namespace Assets.PGKScripts.Interfaces
{
    public interface IWinStreakSource
    {
        int WinStreak { get; set; }
        WinStreakEvent WinStreakChanged { get; }
    }
}
