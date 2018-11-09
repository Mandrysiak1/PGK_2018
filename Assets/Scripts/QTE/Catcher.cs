using System;
using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public class Catcher : MonoBehaviour
    {
        [SerializeField]
        private MonoButtonInput ButtonInput;
        [SerializeField]
        private MonoAnalogInput AnalogInput;

        [Serializable]
        public class CatchPossibleEvent : UnityEvent<GameObject> {}

        public UnityEvent OnCatchAttempt;
        public CatchPossibleEvent OnCatch, OnFail;

        private GameObject PossibleCatch;

        private void Start()
        {
            ButtonInput.Subscribe(InvokeOnCatch);
            if(AnalogInput != null)
                AnalogInput.Subscribe((float dummy) => InvokeOnCatch());
        }

        private void InvokeOnCatch()
        {
            if (!enabled)
                return;
            OnCatchAttempt.Invoke();
            if (PossibleCatch != null)
            {
                OnCatch.Invoke(PossibleCatch);
                PossibleCatch = null;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!enabled)
                return;

            PossibleCatch = other.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!enabled)
                return;
            PossibleCatch = null;
        }
    }
}