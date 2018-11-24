using System.Collections.Generic;
using System.Linq;
using Game.Initialization;
using UnityEngine;

public class OrderSourceProvider : LevelLoadListener
{
    [SerializeField]
    private List<OrderSource> OrderSources = new List<OrderSource>();

    public override void Invoke(GameLevel level, LevelScene levelScene)
    {
        OrderSources.AddRange(FindObjectsOfType<OrderSource>());
    }

    public IEnumerable<OrderSource> Sources
    {
        get
        {
            return OrderSources.Where(source => source.enabled && source.IsActive);
        }
    }
}
