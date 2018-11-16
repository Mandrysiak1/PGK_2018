using Assets.PGKScripts.Perks.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.PGKScripts.Perks.WinStreak
{
    public class Perk : IPerk
    {
        public bool Availible { get; set; }
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

        public IModifiableByPerk UnderlyingObject { get; private set; }

        public string Name { get; set; }

        public Perk(IModifiableByPerk underlyingObject)
        {
            this.UnderlyingObject = underlyingObject;
        }
        public void Invoke(object val)
        {
            UnderlyingObject.Modify(val);
        }
    }
}
