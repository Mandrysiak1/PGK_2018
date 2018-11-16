using Assets.PGKScripts.Perks.Interfaces;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class HoldModif : IModifiableByPerk
    {
        OrderMediator orderMediator;
        public HoldModif(OrderMediator orderMediator)
        {
            this.orderMediator = orderMediator;
        }
        public object GetCurrent()
        {
            return 0;
        }

        public void Modify(object newValue)
        {
            this.orderMediator.MaxBeer = (int)newValue;
        }
    }
}
