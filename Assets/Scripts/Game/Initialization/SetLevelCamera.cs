using UnityEngine;

namespace Game.Initialization
{
    public class SetLevelCamera : LevelLoadListener
    {
        [SerializeField]
        private Camera Camera;

        public override void Invoke(GameLevel level, LevelScene levelScene)
        {
            Transform transform = Camera.transform;
            transform.position = levelScene.CameraStartingPosition.position;
            transform.localRotation = levelScene.CameraStartingPosition.rotation;
        }
    }
}
