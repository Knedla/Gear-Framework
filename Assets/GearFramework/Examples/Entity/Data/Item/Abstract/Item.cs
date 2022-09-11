using GearFramework.Entity.Data;
using System;

namespace GearFramework.Examples.Entity.Data
{
    public abstract class Item<T> : DataEntity, IItem where T : Definition.Item
    {
        [NonSerialized]
        protected static T definition = Database.ItemDatabaseEg.Instance.GetData<T>(); // reminder: for any number of an instances of a type, the Definition is load only once
        protected override GearFramework.Entity.Definition.DataEntity Definition => definition;

        public bool Stackable => definition.Stackable;
        public int BuyQuantityPerUnit => definition.BuyQuantityPerUnit;
    }
}
