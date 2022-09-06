using Entity.Example.Data;
using UnityEngine.UI;

namespace Entity.Example
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
