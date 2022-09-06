using Entity.Definition;
using UnityEngine;

namespace Entity.Example.Definition
{
    public abstract class Currency : DataEntity
    {
        [Header("- Currency Data -")]
        public int MaxAmmount;
    }
}
