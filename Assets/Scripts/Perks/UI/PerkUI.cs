using Assets.Scripts.Perks.Interfaces;
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
        public RawImage grayIcon;
        public Button keyButton;
        protected bool availible = false;
        public bool Availible
        {
            get
            {
                return availible;
            }
            set
            {
                availible = value;
                this.Disable();
            }
        }

        protected bool perkStarted = false;
        public bool PerkStarted
        {
            get
            {
                return perkStarted;
            }
            set
            {
                perkStarted = value;
            }
        }

        public string Name { get; set; }

        public void Start()
        {
            Disable();
            this.StartCoroutine(BloomEffect());
        }

        protected void ChangeTransparency(float value)
        {
            var tempCol = glowIcon.color;
            tempCol.a = value;
            glowIcon.color = tempCol;
        }
        protected float Balance(float v)
        {
            return Mathf.Sin(v);
        }
        protected IEnumerator BloomEffect()
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

        public virtual void Disable()
        {
            icon.enabled = false;
            glowIcon.enabled = false;
            grayIcon.enabled = true;
            keyButton.gameObject.SetActive(false);
        }

        public virtual void Show(PerkStatus status, int time = 0)
        {
            if(PerkStarted)
                ChangeTransparency(0);
            grayIcon.enabled = false;
            icon.enabled = true;
            glowIcon.enabled = true;
            if(status == PerkStatus.Running)
                keyButton.gameObject.SetActive(false);
            else
                keyButton.gameObject.SetActive(true);
        }
    }
}