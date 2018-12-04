using UnityEngine;

public class OrderSourceUI : MonoBehaviour
{
    [SerializeField]
    private OrderSource Source;

    [SerializeField]
    private OrderItemWithAmountUI ItemUI;

    [SerializeField]
    private MoodUI MoodUI;


    private void Start()
    {
        if (Source == null)
            Source = gameObject.GetComponentInParent<OrderSource>();
        Source.OnOrderChanged.AddListener(OnOrderChange);
        Source.OnMoodChanged.AddListener(OnMoodChange);
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
        if(ItemUI != null)
        {
            ItemUI.gameObject.SetActive(active && hasOrder);
            MoodUI.gameObject.SetActive(active);

            if (active)
            {
                if (hasOrder)
                {
                    ItemUI.Item = order.Item;
                    ItemUI.Amount = order.Size - order.FilledSize;
                }

                MoodUI.Mood = Source.Mood;
            }
        }
       
    }
}
