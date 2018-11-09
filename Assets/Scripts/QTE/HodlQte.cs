using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace QTE
{
    public class HodlQte : MonoBehaviour
    {
        public float QteTime = 5.0f;
        public float Power = 1.0f;
        public float PlayerPower = 10.0f;
        public int ChangeChance = 10;
        public float EnemyPowerIncrement = 0.025f;
        public float AnimationTime = 0.5f;

        public MonoButtonInput Left, Right;
        public MonoAnalogInput AnalogHorizontal;


        [SerializeField]
        private Timer Timer;
        [SerializeField]
        private RectTransform Mug;
        [SerializeField]
        private RectTransform Good;
        [SerializeField]
        private RectTransform Bad;

        [SerializeField]
        private Image EndImage;
        [SerializeField]
        private TextMeshProUGUI EndText;

        private Vector3 Position = Vector3.zero;
        private float Sign = 1.0f;
        private UnityAction<bool> OnFinish;
        private bool Finished = false;

        public void Run(UnityAction<bool> onFinished)
        {
            OnFinish = onFinished;
            Timer.OnFinished.AddListener(TimeOver);
            Timer.TimeLeft = QteTime;
            Timer.Run();

            Left.Subscribe(() => Move(-1.0f));
            Right.Subscribe(() => Move(1.0f));
            AnalogHorizontal.Subscribe((float value) => Move(value));
        }

        private void TimeOver()
        {
            Finished = true;
            StartCoroutine(ShowEnd());
        }

        private IEnumerator ShowEnd()
        {
            bool win = Mathf.Abs(Position.x) < Good.rect.width / 2.0f;
            EndText.text = !win ? "LAME" : "GOOD!";
            EndImage.gameObject.SetActive(true);

            EndImage.DOFade(0.0f, AnimationTime)
                .From();
            EndText.DOFade(1.0f, AnimationTime);

            yield return new WaitForSeconds(AnimationTime * 3);

            OnFinish.Invoke(win);
            Destroy(gameObject);
        }

        private void FixedUpdate()
        {
            if (Finished) return;
            if (Mathf.Abs(Position.x) > Good.rect.width / 2.0f)
            {
                Sign = Mathf.Sign(Position.x);
            }
            else if (Random.Range(0, 100) < ChangeChance)
            {
                Sign = Mathf.Sign(Random.value - 0.5f);
            }

            Power += EnemyPowerIncrement;
            ApplyPower(Sign);
        }

        private void Move(float sign)
        {
            if (Finished)
                return;

            float power = PlayerPower;
            ApplyPower(power * sign);
        }

        private void ApplyPower(float sign)
        {
            Position.x += sign * Power;
            float badWidth = Bad.rect.width / 2.0f;
            if (Position.x > badWidth)
                Position.x = badWidth;
            else if (Position.x < -badWidth)
                Position.x = -badWidth;

            Mug.transform.localPosition = Position;
        }
    }
}