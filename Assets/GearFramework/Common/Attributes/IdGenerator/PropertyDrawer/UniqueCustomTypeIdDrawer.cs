#if UNITY_EDITOR

using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    [CustomPropertyDrawer(typeof(UniqueIdByCustomTypeAttribute))]
    class UniqueCustomTypeIdDrawer : UniqueIdGeneratorDrawer
    {
        protected override SerializedPropertyType SerializedPropertyType => SerializedPropertyType.Integer;
        protected override void SetValue(SerializedProperty property) => property.intValue = UniqueIdGenerator.GenerateId(((UniqueIdByCustomTypeAttribute)attribute).Type);
    }
}
#endif
