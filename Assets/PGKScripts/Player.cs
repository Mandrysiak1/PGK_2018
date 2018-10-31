using UnityEngine;

namespace Assets.PGKScripts
{
   public class Player
    {
        private int beersOnPlate = 0;

        public int BeersHandedOut { get; private set; }
        public int MaxBeers { get; set; }

        public Player()
        {
            BeersHandedOut = 0;
            MaxBeers = 5;
        }
        internal void ResetBeersHandedOut()
        {
            this.BeersHandedOut = 0;
        }
        public void RemoveBeer()
        {
            beersOnPlate -= 1;
            Debug.Log("Na tacy znajduje sie: " + beersOnPlate + " piw");
            BeersHandedOut += 1;
        }
        public void AddBeer()
        {
            if(beersOnPlate < MaxBeers)
            {
                beersOnPlate += 1;
            }
            
            Debug.Log("Na tacy znajduje sie: " + beersOnPlate + " piw");
        }
        public int GetBeersOnPlateQuantity()
        {
            return beersOnPlate;
        }
        public void SetBeersOnPlateQuantity(int x)
        {
            beersOnPlate = x;
            Debug.Log("Ustawiono ilośc piw na: " + beersOnPlate);
        }
    }
}
