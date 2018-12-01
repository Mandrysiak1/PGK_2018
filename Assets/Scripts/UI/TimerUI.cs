using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI Text;

    public float Value
    {
        get { return _Value; }
        set
        {
            _Value = value;
            Dirty = true;
        }
    }

    private float _Value = 0;
    private bool Dirty = false;

    private void Update()
    {
        if (Dirty)
        {
            float value = Mathf.Max(0.0f, Value);

            int minutes = (int)value / 60;
            int seconds = (int)value % 60;

            Text.text = string.Format("{0}:{1:D2}", minutes, seconds);
        }
    }
}
