using System;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace QTE
{
    public class Catcher : MonoBehaviour
    {
        [SerializeField]
        private MonoButtonInput ButtonInput;

        public float AnimationTime = 0.15f;


        public Color CanCatchColor = Color.green;
        [SerializeField]
        private Image Background;

        [SerializeField]
        private Image Key;

        [Serializable]
        public class CatchPossibleEvent : UnityEvent<GameObject> {}

        public UnityEvent OnCatchAttempt;
        public CatchPossibleEvent OnCatch;

        private GameObject PossibleCatch;
        private Tweener ShakeTweener;
        private Color InitialBackgroundColor;

        private void Start()
        {
            if(Background != null)
                InitialBackgroundColor = Background.color;
            ButtonInput.Subscribe(InvokeOnCatch);
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

            if (Background != null)
            {
                var sequence = DOTween.Sequence();
                sequence.Append(Background.DOColor(CanCatchColor, AnimationTime));
                sequence.AppendInterval(AnimationTime * 2);
                sequence.Append(Background.DOColor(InitialBackgroundColor, AnimationTime));

                Key.transform.DOScale(2.0f, AnimationTime);
            }

            PossibleCatch = other.gameObject;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!enabled)
                return;

            if (Key != null)
            {
                Key.transform.DOScale(1.0f, AnimationTime);
            }

            PossibleCatch = null;
        }
    }
}