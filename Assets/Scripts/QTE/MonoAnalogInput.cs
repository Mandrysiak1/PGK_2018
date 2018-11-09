using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public abstract class MonoAnalogInput : MonoBehaviour, IAnalogInput
    {
        public abstract void Subscribe(UnityAction<float> onInput);
    }
}