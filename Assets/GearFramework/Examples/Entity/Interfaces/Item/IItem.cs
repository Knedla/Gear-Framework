using GearFramework.Entity.Data;

namespace GearFramework.Examples.Entity.Data
{
    public interface IItem : IDataEntity
    {
        bool Stackable { get; }
        int BuyQuantityPerUnit { get; }
    }
}
