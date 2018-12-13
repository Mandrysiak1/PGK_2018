using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Perks.UI
{
    public class PerkUI : MonoBehaviour, IPerkUi
    {
        public RawImage icon;
        public RawImage glowIcon;
        public Text caption;
        private Color textColor;
        public Button keyButton;
        private bool availible = false;
        public bool Availible
        {
            get
            {
                return availible;
            }
            set
            {
                availible = value;
                if (value == true)
                    PerkStarted = false;
            }
        }
        private bool perkStarted = false;
        public bool PerkStarted
        {
            get
            {
                return perkStarted;
            }
            set
            {
                perkStarted = value;
                if (value == true)
                    ChangeTransparency(0);
            }
        }
        public string Name { get; set; }

        public void Start()
        {
            Disable();
            this.StartCoroutine(BloomEffect());
        }

        private void ChangeTransparency(float value)
        {
            var tempCol = glowIcon.color;
            tempCol.a = value;
            glowIcon.color = tempCol;
        }
        private float Balance(float v)
        {
            return Mathf.Sin(v);
        }
        private IEnumerator BloomEffect()
        {
            float c = 0;
            while(true)
            {
                if (Availible && !PerkStarted)
                {
                    c += Time.deltaTime * 5;
                    if (c >= 3.14)
                        c = 0;
                    ChangeTransparency(Balance(c));
                }
                yield return null;
            }
        }

        public void Update()
        {
            
        }

        public void Disable()
        {
            icon.enabled = false;
            ChangeTransparency(0);
            caption.enabled = false;
            caption.text = "";
            keyButton.gameObject.SetActive(false);
            textColor = caption.color;
        }

        public void Show(string status, Color color)
        {
            icon.enabled = true;
            caption.enabled = true;
            keyButton.gameObject.SetActive(true);
            caption.text = status.ToString();
            if (!color.Equals(textColor))
            {
                textColor = color;
                caption.color = color;
            }
        }
    }
}