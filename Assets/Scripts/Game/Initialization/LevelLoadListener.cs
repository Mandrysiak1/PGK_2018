using UnityEngine;


namespace Game.Initialization
{
    public abstract class LevelLoadListener : MonoBehaviour
    {
        public abstract void Invoke(GameLevel level, LevelScene levelScene);

        // To allow enabling/disabling script from Inspector
        // it must have Start or Update method
        private void Start()
        {
        }
    }
}
