public interface IPerksUi<Status, MType>
{
    void Show(Status status, MType message);
    void Disable();
}