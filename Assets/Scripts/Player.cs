using UnityEngine;

namespace Assets.PGKScripts
{
   public class Player
    {
        public int BeersHandedOut { get; private set; }
        public int MaxItems { get; set; }

        public bool Vulnerable { get; set; }


        private PlayerPlate Plate;
        public Player(PlayerPlate plate)
        {
            if (plate == null)
                Debug.Log("NULL");
            Plate = plate;
            BeersHandedOut = 0;
            MaxItems = 5;
            Vulnerable = true;
        }
        internal void ResetBeersHandedOut()
        {
            this.BeersHandedOut = 0;
        }
        public void RemoveBeer(OrderItem x)
        {
            if(Plate.orderItemsOnPlate[x] > 0)
            {
                Plate.orderItemsOnPlate[x] -= 1;

                //Plate.Beers--;

                BeersHandedOut++; 
            }
            
             Debug.Log("na tacy zostało: " + Plate.orderItemsOnPlate[x]+" " + x.name );
        }
        public void AddOrderItemOnPlate(OrderItem x ){

           
      
                if (Plate.orderItemsOnPlate.ContainsKey(x) == true)
                {
                    if (Plate.orderItemsOnPlate[x] < MaxItems)
                        Plate.orderItemsOnPlate[x] += 1;
                }
                else
                {
                        Plate.orderItemsOnPlate.Add(x, 1);
                }

            
            Debug.Log("Na tacy jest : " + Plate.orderItemsOnPlate[x] + " " + x.name);

        }
        public void RemoveOrderItemOnPlate(OrderItem x)
        {
            Plate.orderItemsOnPlate[x] -= 1;
        }

        public int GetItemOrderOnPlateQuantity(OrderItem x)
        {
            return Plate.orderItemsOnPlate[x];
        }

        public void SetBeersOnPlateQuantity(int x)
        {
            Plate.Beers = x;
            Debug.Log("Ustawiono ilośc piw na: " + x);
        }
    }
}
