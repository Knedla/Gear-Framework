namespace Entity.Example.Data
{
    public interface IAmmo : IItem
    {
        float DamageModifier { get; }
    }
}
