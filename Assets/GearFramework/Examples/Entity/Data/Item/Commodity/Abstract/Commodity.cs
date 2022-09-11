using System;

namespace GearFramework.Examples.Entity.Data
{
    [Serializable]
    public abstract class Commodity<T> : Item<T>, ICommodity where T : Definition.Commodity { }
}
