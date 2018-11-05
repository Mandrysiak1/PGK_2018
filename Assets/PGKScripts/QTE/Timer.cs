using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public class Timer : MonoBehaviour
    {
        public float TimeLeft = 3.0f;
        public UnityEvent OnFinished;

        [SerializeField]
        private TextMeshProUGUI Text;

        private bool Finished = true;

        public void Run()
        {
            Finished = false;
        }

        private void Update()
        {
            if (Finished)
                return;

            TimeLeft -= Time.deltaTime;
            int seconds = (int)TimeLeft;
            int milliseconds = (int)((TimeLeft - seconds) * 100);
            if (milliseconds < 0)
                milliseconds = 0;
            if (seconds < 0)
                seconds = 0;

            Text.text = string.Format("{0}:{1:D2}", seconds, milliseconds);
            if (TimeLeft < 0.0f)
            {
                TimeLeft = 0.0f;
                OnFinished.Invoke();
                Finished = true;
            }
        }
    }
}