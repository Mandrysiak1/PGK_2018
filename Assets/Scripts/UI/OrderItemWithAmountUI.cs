using TMPro;
using UnityEngine;

public class OrderItemWithAmountUI : OrderItemUI
{
    public string AmountFormat = "{0}";

    [SerializeField]
    private int _Amount = 0;

    [SerializeField]
    private TextMeshProUGUI Text;

    private bool Dirty = true;

    private void Update()
    {
        if (Dirty)
        {
            Text.text = string.Format(AmountFormat, _Amount);
            RefreshImage();
            Dirty = false;
        }
    }

    public int Amount
    {
        get { return _Amount; }
        set
        {
            _Amount = value;
            Dirty = true;
        }
    }
}
