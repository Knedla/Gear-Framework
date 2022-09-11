using GearFramework.Examples.Entity.Data;
using UnityEngine.UI;

namespace GearFramework.Examples.Entity
{
    public class ArmorTooltip : Tooltip<IArmor>
    {
        public Text DefenseText;

        public override void SetData(IArmor entity)
        {
            base.SetData(entity);
            DefenseText.text = $"Defense: {entity.Defense}";
        }
    }
}
