using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PGKScripts.Perks.UI
{
    public class SpeedPerkWinStreakUI : MonoBehaviour, IPerksUi<string, Color>
    {
        public RawImage icon;
        public Text caption;
        private Color textColor;
        public void Start()
        {
            icon.enabled = false;
            caption.enabled = false;
            textColor = caption.color;
        }

        public void Update()
        {

        }

        public void Disable()
        {
            icon.enabled = false;
            caption.enabled = false;
            caption.text = "";
        }

        public void Show(string status, Color color)
        {
            icon.enabled = true;
            caption.enabled = true;
            caption.text = status.ToString();
            if (!color.Equals(this.textColor))
            {
                this.textColor = color;
                caption.color = color;
            }
        }
    }
}
