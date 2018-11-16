public interface IPerkUi
{
    string Name { get; set; }
    void Show(string status, UnityEngine.Color color);
    void Disable();
}