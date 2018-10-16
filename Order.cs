using Assets;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Order
    {
        public string ID { get; private set; } 
        private float startTime = 0;
        private float endTime = 0;
        private int orderSize = 0;
        private Table currTable;


        public Order()
        {
            ID = System.Guid.NewGuid().ToString();
        }

        public void AssignOrder(Table table, int ordSize = 1)
        {
            startTime = Time.time;
            orderSize = ordSize;
            currTable = table;
        }

        public override bool Equals(object obj)
        {
            var order = obj as Order;
            return order != null &&
                   ID == order.ID;
        }

        public override int GetHashCode()
        {
            var hashCode = 196815894;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            return hashCode;
        }
    }
}
