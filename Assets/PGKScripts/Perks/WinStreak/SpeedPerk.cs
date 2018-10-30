using Assets.PGKScripts.Perks.Interfaces;

namespace Assets.PGKScripts.Perks.WinStreak
{
    class SpeedPerk : IPerk<float>
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

        public IModifiableByPerk<float> UnderlyingObject { get; private set; }
        SpeedPerk(IModifiableByPerk<float> underlyingObject)
        {
            this.UnderlyingObject = underlyingObject;
        }
    }
}
