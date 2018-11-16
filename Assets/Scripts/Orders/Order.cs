using Assets;
using Assets.PGKScripts.Enums;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets
{
    public class Order 
    {
        public string ID { get; private set; }
        private float startTime;
        public OrderItem orderType { get; set; }
        private int orderSize = 0;
       
        public float getStartTime()
        {
            return startTime;
        }
   
        public float getOrderSize()
        {
            return orderSize;
        }

        public Order(float startTime, OrderItem orderType)
        {
            ID = System.Guid.NewGuid().ToString();
            this.startTime = startTime;

            this.orderType = orderType;

            CalculateOrderSize();
           
        }

        private void CalculateOrderSize()
        {            
                orderSize = UnityEngine.Random.Range(1, orderType.MaximumSize);           
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
