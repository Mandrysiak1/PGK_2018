using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class Table
    {
        public string ID { get; private set; }
        private Order currOrder;
        private bool imWaiting = false;
        public int beersOnTable = 0;
        public float Mood = 20;

    public bool TableAwaiting
        {
            get
            {
                return imWaiting;
            }

            set
            {
                this.imWaiting = value;
                //PERHAPS THROW ORDER HERE TO UI HANDLER?
            }
        }
        public Table()
        {
            ID = System.Guid.NewGuid().ToString();
        }

        public Order CurrOrder
    {
        get
        {
            return currOrder;
        }

        set
        {
            this.currOrder = value;
        }
    }
   
     

        public bool IsThereOrder()
        {
            return (currOrder == null ? false : true);
        }

        public override bool Equals(object obj)
        {
            var table = obj as Table;
            return table != null &&
                   ID == table.ID;
        }

        public override int GetHashCode()
        {
            var hashCode = 753895831;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
            return hashCode;
        }

        public void putBeer()
        {
            
            beersOnTable += 1;
        }

        public int getBOT()
        {
        return beersOnTable;
        }


    public bool ControlOrder(float currentTime, float moodDecreaseValue)
    {
        if (currOrder != null )
        {
            if (beersOnTable >= currOrder.getOrderSize())
            {
                CurrOrder = null;
                beersOnTable = 0;
                return true;
            }
            else
            {
                Mood -= moodDecreaseValue * Time.deltaTime;
            }

        }
        return false;
    }

}
