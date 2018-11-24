using System;
using UnityEngine.Events;

namespace Game.Initialization
{
    public class LevelLoadEvent : LevelLoadListener
    {
        [Serializable]
        public class LevelLoadedEvent : UnityEvent<GameLevel, LevelScene>
        {
        }

        public LevelLoadedEvent LevelLoaded;

        public override void Invoke(GameLevel level, LevelScene levelScene)
        {
            LevelLoaded.Invoke(level, levelScene);
        }
    }
}
