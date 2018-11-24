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
            OrderSource[] sources = FindObjectsOfType<OrderSource>();
            var possibleItems = sources
                .SelectMany(s => s.PossibleRequests)
                .Distinct();

            foreach (OrderItem item in possibleItems)
            {
                PlateUI.IntroduceItem(item);
            }
        }

        public override void Invoke(GameLevel level, LevelScene levelScene)
        {
            IntroduceAll(level, levelScene);
        }
    }
}
