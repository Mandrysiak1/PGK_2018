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
        if (EventSystem == null)
            EventSystem = FindObjectOfType<EventSystem>();
        if (EventSystem != null && EventSystem.currentSelectedGameObject == FirstSelection)
            EventSystem.SetSelectedGameObject(null);
        if(gameObject != null)
            gameObject.SetActive(false);
    }


}
