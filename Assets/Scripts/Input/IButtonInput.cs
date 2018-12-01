using UnityEngine.Events;

namespace QTE
{
    public interface IButtonInput
    {
        void Subscribe(UnityAction onInput);
    }
}