using TMPro;
using UnityEngine;

public class TipCounter : MonoBehaviour
{
    public string Format = "{0}";
    [SerializeField]
    private TextMeshProUGUI Text;

    private int LastTips = 0;
    private bool Dirty = true;

    private void Update()
    {
        if (LastTips != UpgradeClass.Tip)
        {
            Dirty = true;
        }

        if (Dirty)
        {
            LastTips = UpgradeClass.Tip;
            Text.text = string.Format(Format, LastTips);
            Dirty = false;
        }
    }
}
