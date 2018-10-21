using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PGKScripts
{
   public class Player
    {
        private int beersOnPlate = 0;

        public void removeBeer()
        {
            beersOnPlate -= 1;
            Debug.Log("Na tacy znajduje sie: " + beersOnPlate + " piw");
        }
        public void AddBeer()
        {
            if(beersOnPlate <= 4)
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
