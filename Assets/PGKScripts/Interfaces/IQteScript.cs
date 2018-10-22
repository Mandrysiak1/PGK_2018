using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Assets.PGKScripts.Interfaces
{
    public interface IQteScript
    {
        string CurrentChar { get; }
        event PropertyChangedEventHandler PropertyChanged;
        bool Success { get; }
    }
}
