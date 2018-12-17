using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Perks.UI
{
    public class PerkUItut : PerkUI
    {
        public TextMeshProUGUI tutex;

        public override void Disable()
        {
            tutex.enabled = false;
            icon.enabled = false;
            glowIcon.enabled = false;
            caption.enabled = false;
            caption.text = "";
            keyButton.gameObject.SetActive(false);
        }

        public override void Show(string status)
        {
            if (PerkStarted)
                ChangeTransparency(0);
            icon.enabled = true;
            glowIcon.enabled = true;
            caption.enabled = true;
            keyButton.gameObject.SetActive(true);
            caption.text = status.ToString();
            caption.color = Color.green;
            tutex.enabled = true;
        }
    }
}