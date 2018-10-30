using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.PGKScripts.Perks.UI
{
    public class HoldPerkWinStreakUi : MonoBehaviour, IPerksUi<string>
    {
        public RawImage icon;
        public Text caption;
        public void Start()
        {
            icon.enabled = false;
            caption.enabled = false;
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

        public void Show(string status)
        {
            icon.enabled = true;
            caption.enabled = true;
            caption.text = status.ToString();
        }
    }
}

