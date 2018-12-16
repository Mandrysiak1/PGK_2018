public abstract class Order
{
    /// <summary>
    /// 0.0 - 1.0
    /// 0 - player did not do anything
    /// 1 - order fully completed
    /// </summary>
    public abstract float Progress { get; }
    public abstract bool IsFilled { get; }
    /// <summary>
    /// How many times the order needs to be updated to be filled
    /// </summary>
    public abstract int UpdatesToFill { get; }

    public abstract bool CanBeFilled(OrderContext context);
    public abstract bool TryFill(OrderContext context);
    public abstract OrderVisualization Visualize();
}
