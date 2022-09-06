#if UNITY_EDITOR

using System.Reflection;
using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    [CustomPropertyDrawer(typeof(UniqueIdByTypeAttribute))]
    class UniqueIdDrawer : UniqueIdGeneratorDrawer
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
