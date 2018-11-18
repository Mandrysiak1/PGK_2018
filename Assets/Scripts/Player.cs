using UnityEngine;

namespace Assets.PGKScripts
{
    public class Player
    {
        public int BeersHandedOut { get; private set; }
        public int maxOrderSizeModifier { get; set; }

        public bool Vulnerable { get; set; }


        private PlayerPlate Plate;
        public Player(PlayerPlate plate)
        {
            if (plate == null)
                Debug.Log("NULL");
            Plate = plate;
            BeersHandedOut = 0;
            maxOrderSizeModifier = 0;
            Vulnerable = UpgradeClass.Vulnerable;
        }
        internal void ResetBeersHandedOut()
        {
            this.BeersHandedOut = 0;
        }
        public void RemoveBeer(OrderItem x)
        {
            Plate.RemoveItem(x);
            System.Random rnd = new System.Random();
            UpgradeClass.Tip += rnd.Next(3);

        }
        public void AddOrderItemOnPlate(OrderItem x)
        {
            Plate.AddItem(x);
            
            Debug.Log("Na tacy jest : " + Plate.GetItemQuantityOnPlate(x) + " " + x.name);

        }


        public int GetItemOrderOnPlateQuantity(OrderItem x)
        {
            return Plate.GetItemQuantityOnPlate(x);
        }

        public void SetBeersOnPlateQuantity(int x)
        {
            Plate.Beers = x;
            Debug.Log("Ustawiono ilośc piw na: " + x);
        }
    }
}
