using System;

namespace Entity.Example.Data
{
    [Serializable]
    public abstract class Armor<T> : Equipment<T>, IArmor where T : Definition.Armor
    {
        public virtual float Defense => definition.Defense;
    }
}
