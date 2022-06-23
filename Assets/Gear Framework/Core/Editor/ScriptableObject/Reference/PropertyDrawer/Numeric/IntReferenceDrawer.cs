#if UNITY_EDITOR

namespace UnityEditor
{
    using UnityEngine;

    [CustomPropertyDrawer(typeof(Reference<int>))]
    public class IntReferenceDrawer : ReferenceDrawer<int>
    {
        protected override void SetConstatntValue(Rect position, SerializedProperty property)
        {
            int value = property.FindPropertyRelative(constantValueFieldName).intValue;
            string newValue = EditorGUI.TextField(position, value.ToString());
            int.TryParse(newValue, out value);
            property.FindPropertyRelative(constantValueFieldName).intValue = value;
        }
    }
}
#endif