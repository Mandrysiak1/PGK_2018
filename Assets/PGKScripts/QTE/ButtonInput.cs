using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public class ButtonInput : MonoButtonInput
    {
        public string Button;
        private UnityEvent InputEvent = new UnityEvent();

        public override void Subscribe(UnityAction onInput)
        {
            InputEvent.AddListener(onInput);
        }

        private void Update()
        {
            if (Input.GetButtonDown(Button))
            {
                InputEvent.Invoke();
            }
        }
    }
}