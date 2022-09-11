using System;

namespace GearFramework.Examples.Entity.Data
{
    [Serializable]
    public abstract class Weapon<T> : Equipment<T>, IWeapon where T : Definition.Weapon
    {
        public virtual float Damage => definition.Damage;
    }
}
