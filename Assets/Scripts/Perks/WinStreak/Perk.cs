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
        public int Quantity { get; set; }

        public IModifiableByPerk UnderlyingObject { get; private set; }

        public string Name { get; set; }

        public int MinimumToActivate { get; set; }

        public bool Active { get; set; }

        public Perk(IModifiableByPerk underlyingObject, int minimum)
        {
            this.UnderlyingObject = underlyingObject;
            this.MinimumToActivate = minimum;
            Quantity = 0;
            Active = false;
        }
        public void Invoke(object val)
        {
            UnderlyingObject.Modify(val);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;
            return this.Name == ((Perk)obj).Name;
        }
        public static bool operator==(Perk a, Perk b)
        {
            return a.Equals(b);
        }
        public static bool operator !=(Perk a, Perk b)
        {
            return !a.Equals(b);
        }
    }
}
