using UnityEngine.Events;

namespace QTE
{
    public interface IAnalogInput
    {
        void Subscribe(UnityAction<float> onInput);
    }
}