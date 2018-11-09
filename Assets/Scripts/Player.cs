using UnityEngine;

namespace Assets.PGKScripts
{
   public class Player
    {
        public int BeersHandedOut { get; private set; }
        public int MaxBeers { get; set; }

        private PlayerPlate Plate;

        public Player(PlayerPlate plate)
        {
            Plate = plate;
            BeersHandedOut = 0;
            MaxBeers = 5;
        }
        internal void ResetBeersHandedOut()
        {
            this.BeersHandedOut = 0;
        }
        public void RemoveBeer()
        {
            Plate.Beers--;
            Debug.Log("Na tacy znajduje sie: " + Plate.Beers + " piw");
            BeersHandedOut++;
        }
        public void AddBeer()
        {
            if(Plate.Beers < MaxBeers)
            {
                Plate.Beers += 1;
            }

            Debug.Log("Na tacy znajduje sie: " + Plate.Beers + " piw");
        }
        public int GetBeersOnPlateQuantity()
        {
            return Plate.Beers;
        }
        public void SetBeersOnPlateQuantity(int x)
        {
            Plate.Beers = x;
            Debug.Log("Ustawiono ilośc piw na: " + x);
        }
    }
}
