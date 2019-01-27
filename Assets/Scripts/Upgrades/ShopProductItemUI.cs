using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopProductItemUI : MonoBehaviour
{
    public Color ColorUnavailable = Color.red;
    public Color ColorAvailable = Color.green;
    public Action BuyAction;

    [SerializeField]
    public TextMeshProUGUI Text;
    [SerializeField]
    private Button Button;

    private bool _Available = false;

    public bool Inactive
    {
        set
        {
            gameObject.SetActive(value);
        }
    }

    public bool Available
    {
        set
        {
            Text.color = value ? ColorAvailable : ColorUnavailable;
            _Available = value;
        }
        get { return _Available; }
    }

    public void Start()
    {
        Button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        if (Available)
            BuyAction();
    }

}
