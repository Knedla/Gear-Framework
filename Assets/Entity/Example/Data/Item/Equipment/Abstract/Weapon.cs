using System;

namespace Entity.Example.Data
{
    [Serializable]
    public abstract class Weapon<T> : Equipment<T>, IWeapon where T : Definition.Weapon
    {
        public virtual float Damage => definition.Damage;
    }
}
