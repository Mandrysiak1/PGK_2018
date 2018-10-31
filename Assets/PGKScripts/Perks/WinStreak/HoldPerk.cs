using Assets.PGKScripts.Perks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PGKScripts.Perks.WinStreak
{
    public class HoldPerk : IPerk<int>
    {
        int _q;
        public int Quantity
        {
            get
            {
                return _q;
            }

            set
            {
                this._q = value;
            }
        }

        public IModifiableByPerk<int> UnderlyingObject { get; private set; }
        public HoldPerk(IModifiableByPerk<int> underlyingObject)
        {
            this.UnderlyingObject = underlyingObject;
        }
        public void Invoke(int val)
        {
            UnderlyingObject.Modify(val);
        }
    }
}
