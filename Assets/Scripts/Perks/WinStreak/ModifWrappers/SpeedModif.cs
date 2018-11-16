using Assets.PGKScripts.Perks.Interfaces;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.PGKScripts.Perks.WinStreak.ModifWrappers
{
    public class SpeedModif : IModifiableByPerk
    {
        ThirdPersonCharacter character;

        public SpeedModif(ThirdPersonCharacter character)
        {
            this.character = character;
        }
        public object GetCurrent()
        {
            return this.character.getm_MoveSpeedMultiplier();
        }

        public void Modify(object newValue)
        {
            this.character.setm_AnimSpeedMultiplier((float)newValue);
            this.character.setm_MoveSpeedMultiplie((float)newValue);
        }
    }
}