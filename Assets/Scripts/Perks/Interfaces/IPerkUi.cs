using Assets.Scripts.Perks.Interfaces;

public interface IPerkUi
{
    string Name { get; set; }
    bool Availible { get; set; }
    bool PerkStarted { get; set; }
    void Show(PerkStatus status, float time = 0, float topTime = 10);
    void Disable();
}