using System;

namespace Entity.Example.Data
{
    [Serializable]
    public abstract class Equipment<T> : Item<T>, IEquipment where T : Definition.Equipment
    {
        public virtual float Durability { get; }
    }
}
