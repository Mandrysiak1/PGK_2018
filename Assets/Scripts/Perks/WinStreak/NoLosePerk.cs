using Assets.PGKScripts.Perks.Interfaces;
using Assets.PGKScripts.Perks.WinStreak.ModifWrappers;

namespace Assets.PGKScripts.Perks.WinStreak
{
    public class NoLosePerk : IPerk
    {
        public string Name { get; set; }
        public bool Availible { get; set; }
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

        public IModifiableByPerk UnderlyingObject { get; private set; }
        public NoLosePerk(IModifiableByPerk underlyingObject)
        {
            this.UnderlyingObject = underlyingObject;
        }
        public void Invoke(object val)
        {
            UnderlyingObject.Modify(val);
        }
    }
}
