#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    public abstract class ObjectReferenceDrawer<T> : ReferenceDrawer<T>
    {
        protected override void SetConstatntValue(Rect position, SerializedProperty property) => EditorGUI.ObjectField(position, property.FindPropertyRelative(constantValueFieldName), GUIContent.none);
    }
}
#endif
