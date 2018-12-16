public abstract class Order
{
    /// <summary>
    /// 0.0 - 1.0
    /// 0 - player did not do anything
    /// 1 - order fully completed
    /// </summary>
    public abstract float FillRate { get; }
    public abstract bool IsFilled { get; }

    public abstract bool CanBeFilled(OrderContext context);
    public abstract bool TryFill(OrderContext context);
    public abstract OrderVisualization Visualize();
}
