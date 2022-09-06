using Entity.Data;

namespace Entity.Example.Data
{
    public abstract class Currency : DataEntity
    {
        protected abstract Definition.Currency CurrencyDefinition { get; }
        protected override Entity.Definition.DataEntity Definition => CurrencyDefinition;

        public int MaxAmmount => CurrencyDefinition.MaxAmmount;
        public int CurrentAmount = 0;
    }
}
