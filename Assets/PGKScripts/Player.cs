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
        public void addBeer()
        {
            beersOnPlate += 1;
            Debug.Log("Na tacy znajduje sie: " + beersOnPlate + " piw");
        }
        public int getBOP()
        {
            return beersOnPlate;
        }
        public void setBOP(int x)
        {
            beersOnPlate = x;
            Debug.Log("Ustawiono ilośc piw na: " + beersOnPlate);
        }

    

    }
}
