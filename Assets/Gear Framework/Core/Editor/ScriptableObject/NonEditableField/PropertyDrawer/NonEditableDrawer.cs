#if UNITY_EDITOR

using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(NonEditableAttribute))]
    class NonEditableDrawer : PropertyDrawer
    {
        // reminder: https://youtu.be/mTjYA3gC1hA?t=88
        // it is necessary to override CreatePropertiGUI and OnGUI
        // OnGUI is legacy version

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
#endif
