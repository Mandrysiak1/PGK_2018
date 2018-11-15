using Assets.PGKScripts.Perks.Interfaces;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class NoLoseModif : IModifiableByPerk
    {
        Player player;
        public NoLoseModif(Player player)
        {
            this.player = player;
        }
        public object GetCurrent()
        {
            return this.player.Vulnerable;
        }

        public void Modify(object newValue)
        {
            this.player.Vulnerable = (bool)newValue;
        }
    }
}
