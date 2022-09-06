using System;

namespace Entity.Example.Data
{
    [Serializable]
    public abstract class Commodity<T> : Item<T>, ICommodity where T : Definition.Commodity { }
}
