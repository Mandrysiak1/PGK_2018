using Assets.PGKScripts.Perks.Interfaces;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class HoldModif : IModifiableByPerk
    {
        PlayerPlate plate;
        public HoldModif(PlayerPlate plate)
        {
            this.plate = plate;
        }
        public object GetCurrent()
        {
            return 0;
        }

        public void Modify(object newValue)
        {
            this.plate.maximumCapacityMultiplier = (int)newValue;
        }
    }
}