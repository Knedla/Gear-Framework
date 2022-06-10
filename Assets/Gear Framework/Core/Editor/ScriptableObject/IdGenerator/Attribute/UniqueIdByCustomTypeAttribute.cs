using System;

namespace UnityEngine
{
    class UniqueIdByCustomTypeAttribute : PropertyAttribute
    {
        public Type Type { get; }
        public UniqueIdByCustomTypeAttribute(Type type) => Type = type;
    }
}
