using GearFramework.Entity.Data;
using System;

namespace GearFramework.Examples.Entity.Data
{
    [Serializable]
    public class ItemAmount : EntityAmount<IItem>
    {
        public ItemAmount(IItem entity, int amount = 1) : base(entity, amount) { }
    }
}
