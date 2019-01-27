using UnityEngine;
using UnityEngine.EventSystems;

public class GamepadFirstSelection : MonoBehaviour
{
    public EventSystem EventSystem;

    private void Start()
    {
        if (GamepadHelper.IsGamepadPresent())
        {
            if (EventSystem == null)
                EventSystem = FindObjectOfType<EventSystem>();
            EventSystem.SetSelectedGameObject(gameObject);
        }
    }
}
