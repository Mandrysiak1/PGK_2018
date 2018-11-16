using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PGKScripts.Perks.Interfaces;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class HoldModif : IModifiableByPerk
    {
        Player player;
        public HoldModif(Player player)
        {
            this.player = player;
        }
        public object GetCurrent()
        {
            return player.MaxItems;
        }

        public void Modify(object newValue)
        {
            this.player.MaxItems = (int)newValue;
        }
    }
}
