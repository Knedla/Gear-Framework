using System;

namespace GearFramework.Examples.Entity.Data
{
    [Serializable]
    public abstract class Ammo<T> : Item<T>, IAmmo where T : Definition.Ammo
    {
        public virtual float DamageModifier => definition.DamageModifier;
    }
}
