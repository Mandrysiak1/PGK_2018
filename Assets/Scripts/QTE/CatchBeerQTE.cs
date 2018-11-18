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
        public float AnimationTime = 0.5f;
        public float Delay = 0.25f;
        public float FallSpeed = 100.0f;
        public float InitialDelay = 0.75f;

        [SerializeField]
        private RectTransform BeersParent;
        [SerializeField]
        private GameObject BeerPrefab;
        [SerializeField]
        private List<Catcher> Catchers;
        [SerializeField]
        private TextMeshProUGUI CatchedText;
        [SerializeField]
        private HorizontalLayoutGroup CatchersGroup;

        private int Catched = 0;
        private int Missed = 0;
        private Dictionary<GameObject, Catcher> BeerObjToCatcher;
        private PlayerPlateUI PlateUI;
        private PlayerPlate Plate;

        private int LeftToCatch;
        private UnityAction OnEnd;

        public void Run(int beers, PlayerPlate plate, PlayerPlateUI ui, UnityAction onQteEnd)
        {
            OnEnd = onQteEnd;
            ui.transform.SetAsLastSibling();
            Plate = plate;
            PlateUI = ui;

            if (Catchers.Count < beers)
            {
                Debug.LogWarning("Beers count exceed Catchers count");
                beers = Catchers.Count;
            }

            StartCoroutine(RunCo(beers));
        }

        private IEnumerator RunCo(int beers)
        {
            yield return new WaitForSeconds(InitialDelay);
            CatchersGroup.enabled = true;
            Catcher[] usedCatchers = Catchers.Take(beers).ToArray();
            foreach (Catcher catcher in Catchers)
            {
                bool isCatcherActive = usedCatchers.Contains(catcher);
                catcher.gameObject.SetActive(isCatcherActive);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(CatchersGroup.GetComponent<RectTransform>());
            CatchersGroup.enabled = false;

            Catched = Missed = 0;
            LeftToCatch = beers;

            BeerObjToCatcher = new Dictionary<GameObject, Catcher>();

            List<float> delays = Enumerable.Range(0, beers).Select(i => (i + 1) * Delay * Random.Range(1, 2)).ToList();

            foreach (Catcher catcher in usedCatchers)
            {
                GameObject beerObj = Instantiate(BeerPrefab, BeersParent);
                RawImage beerImage = beerObj.GetComponent<RawImage>();
                beerObj.SetActive(true);
                beerObj.transform.localPosition = Vector3.zero;
                Vector3 worldPos = beerObj.transform.position;
                worldPos.x = catcher.transform.position.x;
                beerObj.transform.position = worldPos;

                int delayIdx = UnityEngine.Random.Range(0, delays.Count);
                float delay = delays[delayIdx];
                delays.RemoveAt(delayIdx);

                beerObj.GetComponent<Rigidbody2D>().simulated = false;
                var tween = beerObj.transform.DOMove(PlateUI.transform.position, AnimationTime)
                    .From()
                    .SetDelay(delay);
                beerImage.enabled = false;
                tween.onPlay += () =>
                {
                    beerImage.enabled = true;
                };

                Catcher selfCatcher = catcher;
                // Can't rely on foreach variable in closures
                // because different compiler may handle it as it wishes
                // So just copy it
                tween.onComplete += () =>
                {
                    BeginFall(beerObj);
                    selfCatcher.enabled = true;
                    beerObj.GetComponent<Rigidbody2D>().simulated = true;
                };

                catcher.OnCatch.AddListener(OnCatch);
                catcher.OnCatchAttempt.AddListener(() => OnCatchAttempt(selfCatcher));
                catcher.OnFail.AddListener(OnFail);
                BeerObjToCatcher[beerObj] = catcher;
            }
        }

        private void OnFail(GameObject obj)
        {
            --LeftToCatch;
            ++Missed;
            Destroy(obj);
            TryFinish();
        }

        private void OnCatchAttempt(Catcher catcher)
        {
            catcher.gameObject.SetActive(false);
        }

        private void OnCatch(GameObject beerObj)
        {
            ++Catched;
            --LeftToCatch;
            bool willEnd = LeftToCatch == 0;
            beerObj.GetComponent<Rigidbody2D>().simulated = false;
            beerObj.transform.DOKill();
            var tween = beerObj.transform.DOMove(PlateUI.transform.position, AnimationTime);
            tween.onComplete += () =>
            {
                Destroy(beerObj);
                if(willEnd)
                    TryFinish();
            };
        }

        private void BeginFall(GameObject beerObj)
        {
            Vector3 targetPos = PlateUI.transform.position;
            targetPos.x = beerObj.transform.position.x;
            var tween = beerObj.transform.DOMove(targetPos, FallSpeed)
                .SetEase(Ease.Linear)
                .SetSpeedBased();
            tween.onComplete += () => OnFail(beerObj);
        }

        private void TryFinish()
        {
            if (LeftToCatch <= 0)
            {
                CatchedText.DOFade(1.0f, AnimationTime * 2);
                if (Catched == 0)
                    CatchedText.text = "LAME!";
                else if(Missed == 0)
                    CatchedText.text = "FINE!";
                else
                    CatchedText.text = "NOT BAD!";
                var endTween = CatchedText.DOFade(0.0f, AnimationTime * 2)
                    .SetDelay(AnimationTime * 4);
                endTween.onComplete += () =>
                {
                    Destroy(gameObject);
                    OnEnd.Invoke();
                };
            }
        }
    }
}
