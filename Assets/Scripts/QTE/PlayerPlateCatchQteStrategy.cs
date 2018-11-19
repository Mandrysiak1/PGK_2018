using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPlateCatchQteStrategy : MonoCatchQteStrategy
{
    [Tooltip("True means all items from plate")]
    public bool OnlyOneRandomItem = false;

    [SerializeField]
    private OrderItemUI Prefab;

    [SerializeField]
    protected PlayerPlate Plate;

    [SerializeField]
    private PlayerPlateUI PlateUI;

    protected virtual IEnumerable<OrderItem> Items
    {
        get
        {
            if (OnlyOneRandomItem)
                return new[] { Plate.Items.ToList().Random() };
            else
                return Plate.Items;
        }
    }

    public override IEnumerable<Transform> GetItems(Transform parent)
    {
        foreach (OrderItem item in Items)
        {
            GameObject obj = Instantiate(Prefab.gameObject, parent);
            OrderItemUI itemUi = obj.GetComponent<OrderItemUI>();
            itemUi.Item = item;
            Plate.RemoveItem(item);

            yield return obj.transform;
        }
    }

    public override bool TryGetOrigin(Transform obj, out Vector3 position)
    {
        OrderItemUI orderItemUi = obj.gameObject.GetComponent<OrderItemUI>();
        if (orderItemUi == null)
        {
            position = Vector3.zero;
            return false;
        }

        OrderItem item = orderItemUi.Item;
        OrderItemWithAmountUI plateUi = PlateUI.GetUiFor(item);
        if (plateUi == null)
        {
            position = Vector3.zero;
            return false;
        }

        position = plateUi.transform.position;
        return true;
    }

    public override void Success(Transform item)
    {
        OrderItemUI orderItemUi = item.gameObject.GetComponent<OrderItemUI>();
        Plate.AddItem(orderItemUi.Item);
        Destroy(item.gameObject);
    }

    public override void Fail(Transform item)
    {
        Destroy(item.gameObject);
    }

    public override string GetFinalText(int catched, int missed)
    {
        if (catched == 0)
            return "LAME!";
        else if (missed == 0)
            return "FINE!";
        else
            return "NOT BAD!";
    }
}
