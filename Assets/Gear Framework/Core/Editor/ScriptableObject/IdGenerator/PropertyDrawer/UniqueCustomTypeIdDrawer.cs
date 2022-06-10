#if UNITY_EDITOR

using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(UniqueIdByCustomTypeAttribute))]
    class UniqueCustomTypeIdDrawer : UniqueIdGeneratorPropertyDrawer
    {
        protected override SerializedPropertyType SerializedPropertyType => SerializedPropertyType.Integer;
        protected override void SetValue(SerializedProperty property) => property.intValue = UniqueIdGenerator.GenerateId(((UniqueIdByCustomTypeAttribute)attribute).Type);
    }
}
#endif
