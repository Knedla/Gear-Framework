using System;

namespace Entity.Example.Data
{
    public abstract class Currency<T> : Currency where T : Definition.Currency
    {
        [NonSerialized]
        protected static T definition = Database.CurrencyDatabaseEg.Instance.GetData<T>(); // reminder: for any number of an instances of a type, the Definition is load only once
        protected override Definition.Currency CurrencyDefinition => definition;
    }
}
