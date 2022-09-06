using Entity.Data;
using System;

namespace Entity.Example.Data
{
    [Serializable]
    public class ItemAmount : EntityAmount<IItem>
    {
        public ItemAmount(IItem entity, int amount = 1) : base(entity, amount) { }
    }
}
