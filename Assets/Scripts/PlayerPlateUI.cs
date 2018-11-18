using UnityEngine;
using System.Collections.Generic;

public class PlayerPlateUI : MonoBehaviour
{
    public Color LoseColor = Color.red;
    public Color GainColor = Color.green;

    [SerializeField]
    private OrderItemWithAmountUI OrderItemUiPrefab;
    [SerializeField]
    private PlayerPlate Plate;

    [SerializeField]
    private Transform Content;

    private Dictionary<OrderItem, OrderItemWithAmountUI> ItemUis = new Dictionary<OrderItem, OrderItemWithAmountUI>();

    private void Start()
    {
        Plate.OnItemAmountChanged.AddListener(ItemChanged);
    }

    public void IntroduceItem(OrderItem item)
    {
        if (!ItemUis.ContainsKey(item))
        {
            CreateUi(item);
        }
    }

    private void ItemChanged(OrderItem item, int current, int old)
    {
        OrderItemWithAmountUI ui;
        if (!ItemUis.TryGetValue(item, out ui))
        {
            ui = CreateUi(item);
        }

        ui.Amount = current;
        PlayAnimation(current, old, ui);
    }

    private void PlayAnimation(int current, int old, OrderItemWithAmountUI ui)
    {
        ItemChangedAnimation animation = ui.gameObject.GetComponent<ItemChangedAnimation>();
        if (animation != null)
        {
            Color color;
            if (current < old)
            {
                color = LoseColor;
            }
            else
            {
                color = GainColor;
            }

            animation.StartAnimation(color);
        }
    }

    private OrderItemWithAmountUI CreateUi(OrderItem item)
    {
        OrderItemWithAmountUI ui;
        ui = Instantiate(OrderItemUiPrefab.gameObject, Content).GetComponent<OrderItemWithAmountUI>();
        ui.Item = item;
        ui.Amount = 0;
        ItemUis[item] = ui;
        return ui;
    }
}
