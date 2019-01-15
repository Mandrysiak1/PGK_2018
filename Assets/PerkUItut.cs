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
            icon.enabled = false;
            glowIcon.enabled = false;
            grayIcon.enabled = true;
            keyButton.gameObject.SetActive(false);
            progressBar.fillAmount = 1;
            progressBar.enabled = false;
            tutex.enabled = false;
        }

        public override void Show(PerkStatus status, float time = 0, float topTime = 0)
        {
            if (PerkStarted)
                ChangeTransparency(0);
            grayIcon.enabled = false;
            icon.enabled = true;
            glowIcon.enabled = true;
            if (status == PerkStatus.Running)
            {
                progressBar.enabled = true;
                if (keyButton.gameObject.activeInHierarchy)
                    keyButton.gameObject.SetActive(false);
                if (time != 0.0f)
                {
                    progressBar.fillAmount = time / topTime;
                }
                else
                {
                    progressBar.fillAmount = 0;
                }
            }
            else
            {
                if (!keyButton.gameObject.activeInHierarchy)
                    keyButton.gameObject.SetActive(true);
            }
            tutex.enabled = true;
        }
    }
}