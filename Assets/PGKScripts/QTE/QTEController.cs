using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace QTE
{
    public class QTEController : MonoBehaviour
    {
        [SerializeField]
        private PlayerPlateUI PlateUI;
        [SerializeField]
        private PlayerPlate Plate;
        [SerializeField]
        private ThirdPersonCharacter Character;

        public KeyCode Key;
        public CatchBeerQTE Prefab;

        private float SpeedMultiplier;
        public bool IsRunning { get; private set; }



        private void Start()
        {
            if (Character == null)
            {
                Character = FindObjectOfType<ThirdPersonCharacter>();
            }
        }

        private void Update()
        {
            if (Debug.isDebugBuild && Input.GetKeyDown(Key))
            {
                TryRun();
            }
        }

        public void TryRun()
        {
            if(Plate.Beers > 0)
                Run(Plate.Beers);
        }

        public void Run(int beers)
        {
            IsRunning = true;
            SpeedMultiplier = Character.getm_MoveSpeedMultiplier();
            Character.setm_MoveSpeedMultiplie(0.0f);

            GameObject obj = Instantiate(Prefab.gameObject);
            CatchBeerQTE qte = obj.GetComponent<CatchBeerQTE>();
            obj.transform.SetAsLastSibling();
            qte.Run(beers, Plate, PlateUI, OnQteEnd);
        }

        private void OnQteEnd()
        {
            Character.setm_MoveSpeedMultiplie(SpeedMultiplier);
            IsRunning = false;
        }
    }
}