using UnityEngine;
using UnityEngine.EventSystems;

public class MenuContent : MonoBehaviour
{
    [SerializeField]
    private EventSystem EventSystem = null;

    public GameObject FirstSelection;

    public virtual void Enable()
    {
        gameObject.SetActive(true);
        if (GamepadHelper.IsGamepadPresent())
        {
            if (EventSystem == null)
                EventSystem = FindObjectOfType<EventSystem>();
            EventSystem.SetSelectedGameObject(FirstSelection);
        }
    }

    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }


}
