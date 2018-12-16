using UnityEngine;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{
    [SerializeField]
    private OrderItem _Item;
    [SerializeField]
    private Image Image;

    protected bool Dirty = true;

    protected void Update()
    {
        if (Dirty)
        {
            Refresh();
            Dirty = false;
        }
    }

    protected virtual void Refresh()
    {
        if (_Item == null)
            Image.sprite = null;
        else
            Image.sprite = _Item.Sprite;
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
}
