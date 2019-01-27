using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Serializable]
    public struct ButtonToContent
    {
        public Button Button;
        public MenuContent Content;
    }

    public MenuContent InitialContent = null;

    [SerializeField]
    private List<ButtonToContent> Contents;
    private MenuContent CurrentContent = null;

    private void Start()
    {
        if(InitialContent != null)
            SetContent(InitialContent);
        foreach (ButtonToContent btn2Content in Contents)
        {
            ButtonToContent content = btn2Content;
            btn2Content.Button.onClick.AddListener(() => SetContent(content.Content));
        }
    }

    private void SetContent(MenuContent content)
    {
        if(CurrentContent != null)
            CurrentContent.Disable();
        content.Enable();
        CurrentContent = content;
    }
}
