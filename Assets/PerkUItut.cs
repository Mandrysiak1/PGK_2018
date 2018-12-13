using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Perks.UI
{
    public class PerkUItut : MonoBehaviour, IPerkUi
    {
        public RawImage icon;
        public Text caption;
        public Text tutcaption;
        private Color textColor;
        public Button keyButton;
        public string Name { get; set; }

        public bool Availible { get; set; }

        public bool PerkStarted
        {
            get;
            set;
        }

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
            tutcaption.enabled = false;
            caption.text = "";
            keyButton.gameObject.SetActive(false);
            textColor = caption.color;
        }

        public void Show(string status)
        {
            icon.enabled = true;
            caption.enabled = true;
            tutcaption.enabled = true;
            keyButton.gameObject.SetActive(true);
            caption.text = status.ToString();
                textColor = Color.green;
                caption.color = textColor;
        }
    }
}