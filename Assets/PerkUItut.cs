using Assets.Scripts.Perks.Interfaces;
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
            keyButton.gameObject.SetActive(false);
        }

        public override void Show(PerkStatus status, float time = 0, float topTime = 0)
        {
            if (PerkStarted)
                ChangeTransparency(0);
            icon.enabled = true;
            glowIcon.enabled = true;
            keyButton.gameObject.SetActive(true);
            tutex.enabled = true;
        }
    }
}