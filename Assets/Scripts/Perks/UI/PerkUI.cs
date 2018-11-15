using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Perks.UI
{
    public class PerkUI : MonoBehaviour, IPerkUi
    {
        public RawImage icon;
        public Text caption;
        private Color textColor;
        public Button keyButton;
        public string Name { get; set; }

        public void Start()
        {
            Disable();
        }

        public void Update()
        {

        }

        public void Disable()
        {
            icon.enabled = false;
            caption.enabled = false;
            caption.text = "";
            keyButton.enabled = false;
            textColor = caption.color;
        }

        public void Show(string status, Color color)
        {
            icon.enabled = true;
            caption.enabled = true;
            keyButton.enabled = true;
            caption.text = status.ToString();
            if (!color.Equals(textColor))
            {
                textColor = color;
                caption.color = color;
            }
        }
    }
}