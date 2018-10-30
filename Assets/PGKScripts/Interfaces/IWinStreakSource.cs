using System.ComponentModel;

namespace Assets.PGKScripts.Interfaces
{
    public interface IWinStreakSource : INotifyPropertyChanged
    {
        int WinStreak { get;  }
    }
}
