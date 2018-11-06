using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public class AnalogInput : MonoAnalogInput
    {
        public class AnalogWrapper : UnityEvent<float> { };

        [SerializeField]
        public string InputName;
        [SerializeField]
        private float Dead = 0.08f;

        private UnityEvent<float> InputEvent = new AnalogWrapper();

        public override void Subscribe(UnityAction<float> onInput)
        {
            InputEvent.AddListener(onInput);
        }

        private void Update()
        {
            float position = Input.GetAxis(InputName);
            if(Mathf.Abs(position) > Dead)
                InputEvent.Invoke(Input.GetAxis(InputName));
        }
    }
}