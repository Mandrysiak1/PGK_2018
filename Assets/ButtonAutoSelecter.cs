using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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