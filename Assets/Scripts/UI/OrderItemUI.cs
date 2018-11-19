using UnityEngine;
using UnityEngine.UI;

public class OrderItemUI : MonoBehaviour
{
    [SerializeField]
    private OrderItem _Item;
    [SerializeField]
    private Image Image;

    private bool Dirty = true;

    private void Update()
    {
        if (Dirty)
        {
            RefreshImage();
            Dirty = false;
        }
    }

    protected void RefreshImage()
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
