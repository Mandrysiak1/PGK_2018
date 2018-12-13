public interface IPerkUi
{
    string Name { get; set; }
    bool Availible { get; set; }
    bool PerkStarted { get; set; }
    void Show(string status);
    void Disable();
}