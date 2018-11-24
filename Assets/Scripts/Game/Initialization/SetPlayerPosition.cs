using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Game.Initialization
{
    public class SetPlayerPosition : LevelLoadListener
    {
        [SerializeField]
        private ThirdPersonCharacter Player;

        public override void Invoke(GameLevel level, LevelScene levelScene)
        {
            Player.gameObject.SetActive(true);
            Player.transform.position = levelScene.PlayerStartingPosition.transform.position;
            Player.transform.localRotation = levelScene.PlayerStartingPosition.transform.localRotation;

            Player.setm_AnimSpeedMultiplier(0.85f + 0.25f * UpgradeClass.SpeedModif);
            Player.setm_MoveSpeedMultiplie(0.85f + 0.25f * UpgradeClass.SpeedModif);
        }
    }
}