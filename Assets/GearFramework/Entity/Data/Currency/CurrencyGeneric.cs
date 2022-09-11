using System;

namespace GearFramework.Entity.Data
{
    public abstract class Currency<T> : Currency where T : Definition.Currency
    {
        [NonSerialized]
        protected static T definition = Database.CurrencyDatabase.Instance.GetData<T>(); // reminder: for any number of an instances of a type, the Definition is load only once
        protected override Definition.Currency CurrencyDefinition => definition;
    }
}
