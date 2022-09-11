using GearFramework.Examples.Entity.Data;
using UnityEngine.UI;

namespace GearFramework.Examples.Entity
{
    public class AmmoTooltip : Tooltip<IAmmo>
    {
        public Text DamageModifierText;

        public override void SetData(IAmmo entity)
        {
            base.SetData(entity);
            DamageModifierText.text = $"Modifier: {entity.DamageModifier}";
        }
    }
}
