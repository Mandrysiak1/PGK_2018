using Assets.PGKScripts.Perks.Interfaces;

public interface IPerk
{
    string Name { get; set; }
    int Quantity { get; set; }
    bool Availible { get; set; }
    IModifiableByPerk UnderlyingObject { get;}
    void Invoke(object val);

}