using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using UnityEngine;

public class OrderSourceUI : MonoBehaviour
{
    [SerializeField]
    private OrderSource Source;

    [SerializeField]
    private OrderItemWithAmountUI ItemPrefab;

    [SerializeField]
    private MoodUI MoodUI;

    [SerializeField]
    private Transform ItemsParent;

    private List<OrderItemWithAmountUI> Items = new List<OrderItemWithAmountUI>();


    private void Start()
    {
        if (Source == null)
            Source = gameObject.GetComponentInParent<OrderSource>();
        Source.OnOrderChanged.AddListener(OnOrderChange);
        Source.OnMoodChanged.AddListener(OnMoodChange);
        gameObject.AddComponent<CancelOutParentsScale>();
        Refresh();
    }

    private void OnMoodChange(float mood)
    {
        Refresh();
    }

    private void OnOrderChange(OrderSource source, Order order)
    {
        Refresh();
    }

    private void Refresh()
    {
        bool active = Source.IsActive;
        Order order = Source.CurrentOrder;
        bool hasOrder = order != null;

        MoodUI.gameObject.SetActive(active);

        if (active)
        {
            int item = 0;
            if (hasOrder)
            {
                OrderVisualization visualization = order.Visualize();

                foreach (KeyValuePair<OrderItem, int> kv in visualization.Items)
                {
                    if (item >= Items.Count)
                        CreateNewItem();
                    Items[item].gameObject.SetActive(true);
                    Items[item].Item = kv.Key;
                    Items[item].Amount = kv.Value;
                    ++item;
                }
            }
            for(int i = item; i < Items.Count; i++)
                Items[i].gameObject.SetActive(false);

            MoodUI.Mood = Source.Mood;
        }
    }

    private void CreateNewItem()
    {
        GameObject obj = Instantiate(ItemPrefab.gameObject, ItemsParent);
        Items.Add(obj.GetComponent<OrderItemWithAmountUI>());
    }
}
