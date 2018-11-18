using System.Net.Mime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderItemWithAmountUI : MonoBehaviour
{
    public string AmountFormat = "{0}";

    [SerializeField]
    private OrderItem _Item;
    [SerializeField]
    private int _Amount = 0;

    [SerializeField]
    private TextMeshProUGUI Text;
    [SerializeField]
    private Image Image;

    private bool Dirty = true;

    private void Update()
    {
        if (Dirty)
        {
            Text.text = string.Format(AmountFormat, _Amount);
            if (_Item == null)
                Image.sprite = null;
            else
                Image.sprite = _Item.Sprite;
            Dirty = false;
        }
    }

    public OrderItem Item
    {
        get { return _Item; }
        set
        {
            _Item = value;
            Dirty = true;
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
