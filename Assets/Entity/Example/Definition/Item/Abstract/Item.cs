using Entity.Definition;
using UnityEngine;

namespace Entity.Example.Definition
{
    public abstract class Item : DataEntity
    {
        [Header("- Item Data -")]
        public bool Stackable;
        public int BuyQuantityPerUnit;
    }
}
