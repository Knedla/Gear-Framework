#if UNITY_EDITOR

using System;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(GUIDGeneratorAttribute))]
    class GUIDGeneratorDrawer : UniqueIdGeneratorPropertyDrawer
    {
        protected override SerializedPropertyType SerializedPropertyType => SerializedPropertyType.String;
        protected override void SetValue(SerializedProperty property) => property.stringValue = Guid.NewGuid().ToString();
    }
}
#endif
