#if UNITY_EDITOR

namespace UnityEditor
{
    using UnityEngine;

    public abstract class ObjectReferenceDrawer<T> : ReferenceDrawer<T>
    {
        protected override void SetConstatntValue(Rect position, SerializedProperty property) => EditorGUI.ObjectField(position, property.FindPropertyRelative(constantValueFieldName), GUIContent.none);
    }
}
#endif