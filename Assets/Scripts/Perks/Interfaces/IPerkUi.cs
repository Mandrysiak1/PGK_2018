using Assets.Scripts.Perks.Interfaces;

public interface IPerkUi
{
    string Name { get; set; }
    bool Availible { get; set; }
    bool PerkStarted { get; set; }
    void Show(PerkStatus status, int time = 0);
    void Disable();
}