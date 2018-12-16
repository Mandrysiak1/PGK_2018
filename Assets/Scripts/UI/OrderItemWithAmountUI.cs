using TMPro;
using UnityEngine;

public class OrderItemWithAmountUI : OrderItemUI
{
    public string AmountFormat = "{0}";

    [SerializeField]
    private int _Amount = 0;

    [SerializeField]
    private TextMeshProUGUI Text;

    protected new void Update()
    {
        base.Update();
    }

    protected override void Refresh()
    {
        base.Refresh();
        Text.text = string.Format(AmountFormat, _Amount);
    }

    public int Amount
    {
        get { return _Amount; }
        set
        {
            if (_Amount != value)
            {
                Dirty = true;
                _Amount = value;
            }
        }
    }
}
