using System;

namespace GearFramework.Examples.Entity.Data
{
    [Serializable]
    public partial class Axe : Weapon<Definition.Axe>
    {
        float damage;
        public override float Damage => damage;

        public Axe() => damage = UnityEngine.Random.Range((int)definition.MinDamage, (int)definition.MaxDamage);
    }
}
