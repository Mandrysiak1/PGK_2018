using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PGKScripts.Perks.Interfaces
{
    public interface IModifiableByPerk
    {
        void Modify(object newValue);
        object GetCurrent();
    }
}
