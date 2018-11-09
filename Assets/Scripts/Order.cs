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
       
        public float getStartTime()
        {
            return startTime;
        }
        public float getEndTime()
        {
            return endTime;
        }
        public float getOrderSize()
        {
            return orderSize;
        }

        public Order(float startTime, float endTime,int orderSize)
        {
            ID = System.Guid.NewGuid().ToString();
            this.startTime = startTime;
            this.endTime = endTime;
            this.orderSize = orderSize;
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
