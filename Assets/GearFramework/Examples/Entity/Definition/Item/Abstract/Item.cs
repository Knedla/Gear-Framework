using GearFramework.Entity.Definition;
using UnityEngine;

namespace GearFramework.Examples.Entity.Definition
{
    public abstract class Item : DataEntity
    {
        [Header("- Item Data -")]
        public bool Stackable;
        public int BuyQuantityPerUnit;
    }
}
