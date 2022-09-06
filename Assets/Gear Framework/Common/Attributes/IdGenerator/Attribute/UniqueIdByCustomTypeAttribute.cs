using System;
using UnityEngine;

namespace GearFramework.Common
{
    public class UniqueIdByCustomTypeAttribute : PropertyAttribute
    {
        public Type Type { get; }
        public UniqueIdByCustomTypeAttribute(Type type) => Type = type;
    }
}
