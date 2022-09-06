using Entity.Example.Data;
using UnityEngine.UI;

namespace Entity.Example
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
