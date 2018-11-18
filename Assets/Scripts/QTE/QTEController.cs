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
        [SerializeField]
        private CatchBeerQTE CatchBeerQte;
        [SerializeField]
        private HodlQte HodlQte;
        [SerializeField]
        private OrderItem HoldQteItem;

        [SerializeField]
        private MainScript main;

        public KeyCode Key;
        public bool IsRunning { get; private set; }

        private float SpeedMultiplier;

        private void Start()
        {
            if (Character == null)
            {
                Character = FindObjectOfType<ThirdPersonCharacter>();
            }
            if (main == null)
                main = FindObjectOfType<MainScript>();
        }

        private void Update()
        {
            if (Debug.isDebugBuild && Input.GetKeyDown(Key))
            {
                TryRun();
            }
        }
        /// <summary>
        /// ADRIAN: Added this if statement but we will have to work with Player to be more independent script itself
        /// so we'll be able to assign it with no need of dependency to MainScript
        /// </summary>
        public void TryRun()
        {
            int holdItemAmount = Plate.GetItemQuantityOnPlate(HoldQteItem);
            if(holdItemAmount > 0 && !main.GetPlayer().Vulnerable)
            {
                Run(holdItemAmount);
            }
            else
            {
                Plate.RemoveRandomItem();
            }
        }

        public void Run(int beers)
        {
            IsRunning = true;
            SpeedMultiplier = Character.getm_MoveSpeedMultiplier();
            Character.setm_MoveSpeedMultiplie(0.0f);

            if (Random.Range(0, 100) < 50)
                RunCatchQte(beers);
            else
                RunHodlQte(beers);
        }

        private void RunHodlQte(int beers)
        {
            GameObject obj = Instantiate(HodlQte.gameObject);
            HodlQte qte = obj.GetComponent<HodlQte>();
            obj.transform.SetAsLastSibling();

            qte.Run((success) =>
            {
                if (!success)
                {
                    int holdItemAmount = Plate.GetItemQuantityOnPlate(HoldQteItem);
                    Plate.RemoveItem(HoldQteItem, holdItemAmount);
                }
                OnQteEnd();
            });
        }

        private void RunCatchQte(int beers)
        {
            GameObject obj = Instantiate(CatchBeerQte.gameObject);
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