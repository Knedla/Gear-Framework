using GearFramework.Examples.Entity.Data;
using UnityEngine.UI;

namespace GearFramework.Examples.Entity
{
    public class WeaponTooltip : Tooltip<IWeapon>
    {
        public Text DamageText;

        public override void SetData(IWeapon entity)
        {
            base.SetData(entity);
            DamageText.text = $"Damage: {entity.Damage}";
        }
    }
}
