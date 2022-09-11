using System;

namespace GearFramework.Examples.Entity.Data
{
    [Serializable]
    public abstract class Armor<T> : Equipment<T>, IArmor where T : Definition.Armor
    {
        public virtual float Defense => definition.Defense;
    }
}
