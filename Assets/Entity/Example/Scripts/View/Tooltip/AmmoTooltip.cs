using Entity.Example.Data;
using UnityEngine.UI;

namespace Entity.Example
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
