using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public class AnalogInput : MonoAnalogInput
    {
        public class AnalogWrapper : UnityEvent<float> { };

        public enum AnalogInputMode
        {
            All,
            OnlyPositive,
            OnlyNegative
        }

        [SerializeField]
        public string InputName;
        [SerializeField]
        private float Dead = 0.08f;
        [SerializeField]
        private AnalogInputMode InputMode = AnalogInputMode.All;

        private UnityEvent<float> InputEvent = new AnalogWrapper();

        public override void Subscribe(UnityAction<float> onInput)
        {
            InputEvent.AddListener(onInput);
        }

        private void Update()
        {
            float position = Input.GetAxis(InputName);
            if (Mathf.Abs(position) > Dead)
            {
                if((InputMode == AnalogInputMode.OnlyNegative && position < 0f)
                    || (InputMode == AnalogInputMode.OnlyPositive && position > 0f)
                    || (InputMode == AnalogInputMode.All)
                    )
                    InputEvent.Invoke(Input.GetAxis(InputName));
            }
        }
    }
}