using System;

namespace UnityEngine
{
    public class UniqueIdByCustomTypeAttribute : PropertyAttribute
    {
        public Type Type { get; }
        public UniqueIdByCustomTypeAttribute(Type type) => Type = type;
    }
}
