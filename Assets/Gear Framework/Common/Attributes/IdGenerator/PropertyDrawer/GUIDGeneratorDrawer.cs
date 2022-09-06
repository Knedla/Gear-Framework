#if UNITY_EDITOR

using System;
using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    [CustomPropertyDrawer(typeof(GUIDGeneratorAttribute))]
    class GUIDGeneratorDrawer : UniqueIdGeneratorDrawer
    {
        protected override SerializedPropertyType SerializedPropertyType => SerializedPropertyType.String;
        protected override void SetValue(SerializedProperty property) => property.stringValue = Guid.NewGuid().ToString();
    }
}
#endif
