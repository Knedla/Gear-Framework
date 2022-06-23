#if UNITY_EDITOR

namespace UnityEditor
{
    using UnityEngine;

    [CustomPropertyDrawer(typeof(Reference<string>))]
    public class StringReferenceDrawer : ReferenceDrawer<string>
    {
        protected override void SetConstatntValue(Rect position, SerializedProperty property) => property.FindPropertyRelative(constantValueFieldName).stringValue = EditorGUI.TextField(position, property.FindPropertyRelative(constantValueFieldName).stringValue);
    }
}
#endif