using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game.Gameplay
{
    class AddTipOnFilledOrder : MonoBehaviour
    {
        public int TipMin = 1;
        public int TipMax = 3;
        public OrderController Orders;

        private void Start()
        {
            Orders.OrderFilled.AddListener(Filled);
        }

        private void Filled(OrderSource arg0, Order arg1)
        {
            UpgradeClass.Tip += UnityEngine.Random.Range(TipMin, TipMax + 1);
        }
    }
}
