using Assets.PGKScripts.Perks.Interfaces;

public interface IPerk<TVal>
{
    int Quantity { get; set; }
    IModifiableByPerk<TVal> UnderlyingObject { get;}

}