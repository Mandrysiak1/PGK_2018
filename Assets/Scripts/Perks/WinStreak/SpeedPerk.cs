using Assets.PGKScripts.Perks.Interfaces;
using Assets.PGKScripts.Perks.WinStreak.ModifWrappers;

namespace Assets.PGKScripts.Perks.WinStreak
{
    public class SpeedPerk : IPerk<float>
    {
        int _q;
        private SpeedModif speedModif;

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

        public IModifiableByPerk<float> UnderlyingObject { get; private set; }
        public SpeedPerk(IModifiableByPerk<float> underlyingObject)
        {
            this.UnderlyingObject = underlyingObject;
        }
        public void Invoke(float val)
        {
            UnderlyingObject.Modify(val);
        }
    }
}
