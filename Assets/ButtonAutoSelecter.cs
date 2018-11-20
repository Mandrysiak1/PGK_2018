using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/*
public class ButtonAutoSelecter : MonoBehaviour
{
    private List<Button> buttons;

    void Start ()
    {
        buttons = FindObjectsOfType<Button>().ToList();
    }
    void Update ()
    {
        if (EventSystem.current.currentSelectedGameObject == null || EventSystem.current.currentSelectedGameObject.activeInHierarchy)
        {
            foreach(Button button in buttons)
            {
                if (button.enabled)
                {
                    EventSystem.current.SetSelectedGameObject(button.gameObject);
                    break;
                }
            }
        }
    }
}
*/
public class ButtonAutoSelecter : MonoBehaviour
{
    [SerializeField]
    private EventSystem EventSystem;

    private void Update()
    {
        var current = EventSystem.currentSelectedGameObject;
        if (current == null || !current.activeInHierarchy)
        {
            Button firstButton = FindObjectOfType<Button>();
            if (firstButton != null)
                EventSystem.SetSelectedGameObject(firstButton.gameObject);
        }
    }

}