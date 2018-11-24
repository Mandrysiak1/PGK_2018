using UnityEngine;

namespace Game.Initialization
{
    public class PlayLevelMusic : LevelLoadListener
    {
        [SerializeField]
        private AudioSource Source;

        public override void Invoke(GameLevel level, LevelScene levelScene)
        {
            Source.clip = level.Music;
            Source.time = 0;
            Source.Play();
        }
    }
}
