namespace GearFramework.Entity.Data
{
    public abstract class Currency : DataEntity
    {
        protected abstract Definition.Currency CurrencyDefinition { get; }
        protected override Definition.DataEntity Definition => CurrencyDefinition;

        public int MaxAmmount => CurrencyDefinition.MaxAmmount;

        int currentAmount;
        public int CurrentAmount { get => currentAmount; set => currentAmount = value; }
    }
}
