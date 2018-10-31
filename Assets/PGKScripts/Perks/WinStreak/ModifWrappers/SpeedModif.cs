using Assets.PGKScripts.Perks.Interfaces;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class SpeedModif : IModifiableByPerk<float>
    {
        ThirdPersonCharacter character;

        public SpeedModif(ThirdPersonCharacter character)
        {
            this.character = character;
        }
        public float GetCurrent()
        {
            return this.character.getm_MoveSpeedMultiplier();
        }

        public void Modify(float newValue)
        {
            this.character.setm_AnimSpeedMultiplier(newValue);
            this.character.setm_MoveSpeedMultiplie(newValue);
        }
    }
}