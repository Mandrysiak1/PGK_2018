using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PGKScripts.Perks.Interfaces;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class HoldModif : IModifiableByPerk<int>
    {
        Player player;
        public HoldModif(Player player)
        {
            this.player = player;
        }
        public int GetCurrent()
        {
            return this.player.MaxBeers;
        }

        public void Modify(int newValue)
        {
            this.player.MaxBeers = newValue;
        }
    }
}
