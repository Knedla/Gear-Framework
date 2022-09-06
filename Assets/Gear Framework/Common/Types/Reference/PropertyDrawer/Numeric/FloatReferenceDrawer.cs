#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    [CustomPropertyDrawer(typeof(Reference<float>))]
    public class FloatReferenceDrawer : ReferenceDrawer<float>
    {
        protected override void SetConstatntValue(Rect position, SerializedProperty property)
        {
            float value = property.FindPropertyRelative(constantValueFieldName).floatValue;
            string newValue = EditorGUI.TextField(position, value.ToString());
            float.TryParse(newValue, out value);
            property.FindPropertyRelative(constantValueFieldName).floatValue = value;
        }
    }
}
#endif
