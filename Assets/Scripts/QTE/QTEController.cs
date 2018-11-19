using System;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using Random = UnityEngine.Random;

namespace QTE
{
    public class QTEController : MonoBehaviour
    {
        public int TipQteChance = 5;
        public int MinimumTipAmount = 3;
        public int MaximumTipAmount = 6;
        [SerializeField]
        private PlayerPlate Plate;
        [SerializeField]
        private ThirdPersonCharacter Character;
        [SerializeField]
        private CatchBeerQTE CatchBeerQte;
        [SerializeField]
        private HodlQte HodlQte;
        [SerializeField]
        private PlayerPlateCatchQteStrategy PlateCatchStrategy;
        [SerializeField]
        private TipCatchQteStrategy TipCatchStrategy;

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
                TryRunCollisionQte();
            }
        }

        public void TryRunTipQte()
        {
            int tipAmount = Random.Range(MinimumTipAmount, MaximumTipAmount + 1);
            TryRunTipQte(tipAmount);
        }

        public void TryRunTipQte(int tipAmount)
        {
            if (Random.Range(0, 100) < TipQteChance)
            {
                TipCatchStrategy.Amount = tipAmount;
                TryRunCatchWithStrategy(TipCatchStrategy);
            }
        }

        public bool TryRunCollisionQte(bool allItems = true)
        {
            int holdItemAmount = Plate.Items.Count();

            if (holdItemAmount > 0)
            {
                PlateCatchStrategy.OnlyOneRandomItem = !allItems;
                TryRunCatchWithStrategy(PlateCatchStrategy);
                return true;
            }

            return false;
        }

        private void TryRunCatchWithStrategy(ICatchQteStrategy strategy)
        {
            if (IsRunning)
                return;

            GameObject obj = Instantiate(CatchBeerQte.gameObject);
            CatchBeerQTE qte = obj.GetComponent<CatchBeerQTE>();
            obj.transform.SetAsLastSibling();

            StopCharacter();
            qte.Run(strategy, OnQteEnd);
            IsRunning = true;
        }

        private void StopCharacter()
        {
            SpeedMultiplier = Character.getm_MoveSpeedMultiplier();
            Character.setm_MoveSpeedMultiplie(0.0f);
        }


        private void RunHodlQte()
        {
            GameObject obj = Instantiate(HodlQte.gameObject);
            HodlQte qte = obj.GetComponent<HodlQte>();
            obj.transform.SetAsLastSibling();

            qte.Run(success =>
            {
                if (!success)
                {
                    Plate.RemoveRandomItem();
                }
                OnQteEnd();
            });
        }

        private void OnQteEnd()
        {
            Character.setm_MoveSpeedMultiplie(SpeedMultiplier);
            IsRunning = false;
        }
    }
}