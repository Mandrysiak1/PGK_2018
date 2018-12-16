using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Initialization
{
    public class IntroduceOrderItems : LevelLoadListener
    {
        [SerializeField]
        private PlayerPlateUI PlateUI;

        private void IntroduceAll(GameLevel level, LevelScene levelScene)
        {
            OrderItem[] possibleItems = FindObjectsOfType<BarScript>()
                .Select(s => s.orderToPick)
                .Distinct()
                .ToArray();

            foreach (OrderItem item in possibleItems)
            {
                if(item != null)
                    PlateUI.IntroduceItem(item);
            }
        }

        public override void Invoke(GameLevel level, LevelScene levelScene)
        {
            IntroduceAll(level, levelScene);
        }
    }
}
