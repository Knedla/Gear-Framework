using System;

namespace GearFramework.Entity.Data
{
    [Serializable]
    public class CurrencyAmount : EntityAmount<Currency>
    {
        public CurrencyAmount(Currency entity, int amount = 1) : base(entity, amount) { }
    }
}
