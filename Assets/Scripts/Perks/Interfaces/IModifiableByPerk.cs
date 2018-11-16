namespace Assets.PGKScripts.Perks.Interfaces
{
    public interface IModifiableByPerk
    {
        void Modify(object newValue);
        object GetCurrent();
    }
}
