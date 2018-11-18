using System.Linq;
using UnityEngine;

public class OrderItemsIntroducer : MonoBehaviour
{
    [SerializeField]
    private LevelLoadListener LevelLoadListener;
    [SerializeField]
    private PlayerPlateUI PlateUI;

    private void Awake()
    {
        LevelLoadListener.LevelLoaded.AddListener(IntroduceAll);
    }

    private void IntroduceAll(GameLevel level, LevelScene levelScene)
    {
        OrderSource[] sources = FindObjectsOfType<OrderSource>();
        var possibleItems = sources
            .SelectMany(s => s.possibleRequests)
            .Distinct();

        foreach (OrderItem item in possibleItems)
        {
            PlateUI.IntroduceItem(item);
        }
    }
}
