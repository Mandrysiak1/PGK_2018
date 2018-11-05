using UnityEngine;
using UnityEngine.Events;

namespace QTE
{
    public abstract class MonoButtonInput : MonoBehaviour, IButtonInput
    {
        public abstract void Subscribe(UnityAction onInput);
    }
}