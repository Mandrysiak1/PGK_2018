using System.Collections.Generic;
using UnityEngine;

public class PlayerPlateCatchQteStrategy : MonoCatchQteStrategy
{
    [SerializeField]
    private OrderItemUI Prefab;

    [SerializeField]
    private PlayerPlate Plate;

    [SerializeField]
    private PlayerPlateUI PlateUI;


    public override IEnumerable<Transform> GetItems(Transform parent)
    {
        foreach (OrderItem item in Plate.Items)
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
