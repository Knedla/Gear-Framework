using Entity.Data;

namespace Entity.Example.Data
{
    public interface IItem : IDataEntity
    {
        bool Stackable { get; }
        int BuyQuantityPerUnit { get; }
    }
}
