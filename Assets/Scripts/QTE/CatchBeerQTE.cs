using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace QTE
{
    public class CatchBeerQTE : MonoBehaviour
    {
        public float TextAnimationTime = 0.25f;
        public float ItemWaitTime = 0.25f;
        public float FromOriginToCenterTime = 0.33f;

        public float FallSpeed = 100.0f;
        public float InitialDelay = 0.75f;
        public float ItemDelay = 0.33f;

        [SerializeField]
        private RectTransform BeersParent;
        [SerializeField]
        private List<Catcher> Catchers;
        [SerializeField]
        private TextMeshProUGUI CatchedText;

        private int Catched = 0;
        private int Missed = 0;
        private int AllToCatch;

        private ICatchQteStrategy _strategy;
        private List<Transform> HandledItems = new List<Transform>();

        private UnityAction OnEnd;

        public void Run(ICatchQteStrategy strategy, UnityAction onQteEnd)
        {
            _strategy = strategy;
            OnEnd = onQteEnd;

            StartCoroutine(RunCo());
        }

        private IEnumerator RunCo()
        {
            yield return new WaitForSeconds(InitialDelay);

            Catched = Missed = 0;
            AllToCatch = 0;
            HandledItems.Clear();

            Catchers.ForEach(InitializeCatcher);

            foreach (Transform uiItem in _strategy.GetItems(BeersParent))
            {
                ThrowItemTo(uiItem, Catchers.Random());
                ++AllToCatch;

                yield return new WaitForSeconds(ItemDelay);
            }
        }

        private void InitializeCatcher(Catcher catcher)
        {
            catcher.OnCatch.RemoveAllListeners();
            catcher.OnCatch.AddListener(obj => CatchItem(obj.transform));
        }

        private void ThrowItemTo(Transform uiTransform, Catcher catcher)
        {
            uiTransform.localPosition = Vector3.zero;
            Sequence seq = DOTween.Sequence();

            Vector3 originPosition;
            Vector3 centerToCatcher = catcher.transform.position - uiTransform.position;
            if(_strategy.TryGetOrigin(uiTransform, out originPosition))
            {
                var animateFromPlateToCenter = uiTransform
                    .DOMove(originPosition, FromOriginToCenterTime)
                    .From();
                seq.Append(animateFromPlateToCenter);
                seq.AppendInterval(ItemWaitTime);
            }

            float magnitude = centerToCatcher.magnitude;

            var animateByDistanceToCatcherFromCenter = uiTransform
                .DOBlendableMoveBy(centerToCatcher * 1.6f, magnitude / FallSpeed)
                .SetEase(Ease.Linear);

            seq.Append(animateByDistanceToCatcherFromCenter);
            seq.AppendCallback(() =>
            {
                if (!HandledItems.Contains(uiTransform))
                    ItemFailed(uiTransform);
            });
            seq.Play();
        }

        private void CatchItem(Transform itemTransform)
        {
            Catched++;
            _strategy.Success(itemTransform);
            TryFinish();
            HandledItems.Add(itemTransform);
        }

        private void ItemFailed(Transform itemTransform)
        {
            Missed++;
            _strategy.Fail(itemTransform);
            TryFinish();
            HandledItems.Add(itemTransform);
        }

        private void TryFinish()
        {
            if (Catched + Missed >= AllToCatch)
            {
                var seq = DOTween.Sequence();

                seq.Append(CatchedText.DOFade(1.0f, TextAnimationTime));

                CatchedText.text = _strategy.GetFinalText(Catched, Missed);
                seq.AppendInterval(TextAnimationTime * 3);
                seq.Append(CatchedText .DOFade(0.0f, TextAnimationTime));
                seq.AppendCallback(() =>
                {
                    OnEnd.Invoke();
                    Destroy(gameObject);
                });
                seq.Play();
            }
        }
    }
}
