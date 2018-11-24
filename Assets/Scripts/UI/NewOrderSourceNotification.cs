using UnityEngine;

public class NewOrderSourceNotification : Notification
{
    public string Format = "New guests have arrived!";
    [SerializeField]
    private OrderSource[] Sources;

    protected new void Start()
    {
        base.Start();
        foreach (OrderSource source in Sources)
        {
            OrderSource sourceCopy = source;
            source.OnActivate.AddListener(() => OnActivate(sourceCopy));
        }
    }

    private void OnActivate(OrderSource source)
    {
        Show(string.Format(Format, source.name));
    }
}
