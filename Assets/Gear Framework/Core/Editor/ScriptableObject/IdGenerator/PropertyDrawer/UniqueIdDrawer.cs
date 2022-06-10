#if UNITY_EDITOR

using System.Reflection;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(UniqueIdByTypeAttribute))]
    class UniqueIdDrawer : UniqueIdGeneratorPropertyDrawer
    {
        protected override SerializedPropertyType SerializedPropertyType => SerializedPropertyType.Integer;
        protected override void SetValue(SerializedProperty property)
        {
            FieldInfo fieldInfo = GetFieldInfo(property.serializedObject.targetObject.GetType(), property.name);
            property.intValue = UniqueIdGenerator.GenerateId(fieldInfo.DeclaringType);
        }
    }
}
#endif
