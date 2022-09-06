using UnityEngine;

namespace GearFramework.Common
{
    public abstract class Variable<T> : ScriptableObject
    {
        public T Value;
    }
}
