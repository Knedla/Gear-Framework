#if UNITY_EDITOR

namespace UnityEditor
{
    using System.Linq;
    using UnityEngine;

    public abstract class ReferenceDrawer<T> : PropertyDrawer
    {
        readonly static Vector2 dropdownButtonSize = Vector2.one * 20;
        const string dropdownTextureName = "d_icon dropdown";
        const float dropdownFixedWidth = 50;
        readonly static RectOffset dropdownBorder = new RectOffset(1, 1, 1, 1);
        const string constantButtondName = "Constant";
        const string variableButtondName = "Variable";
        const int rightOffset = 15;

        protected const string constantValueFieldName = "constantValue";
        const string useConstantFieldName = "useConstant";
        const string variableFieldName = "variable";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            bool useConstatnt = property.FindPropertyRelative(useConstantFieldName).boolValue;

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            if (EditorGUI.DropdownButton(new Rect(position.position, dropdownButtonSize), new GUIContent(GetTexture()), FocusType.Keyboard, new GUIStyle() { fixedWidth = dropdownFixedWidth, border = dropdownBorder }))
            {
                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent(constantButtondName), useConstatnt, () => SetProperty(property, true));
                menu.AddItem(new GUIContent(variableButtondName), !useConstatnt, () => SetProperty(property, false));
                menu.ShowAsContext();
            }

            position.position -= Vector2.right * (rightOffset * (EditorGUI.indentLevel - 1));
            position.size += Vector2.right * (rightOffset * (EditorGUI.indentLevel - 1));

            if (useConstatnt)
                SetConstatntValue(position, property);
            else
                EditorGUI.ObjectField(position, property.FindPropertyRelative(variableFieldName), typeof(Variable<T>), GUIContent.none);

            EditorGUI.EndProperty();
        }

        protected abstract void SetConstatntValue(Rect position, SerializedProperty property);

        private void SetProperty(SerializedProperty property, bool value)
        {
            SerializedProperty propertyRelative = property.FindPropertyRelative(useConstantFieldName);
            propertyRelative.boolValue = value;
            propertyRelative.serializedObject.ApplyModifiedProperties();
        }

        private Texture GetTexture()
        {
            Texture texture = (Texture)UnityEngine.Resources.FindObjectsOfTypeAll(typeof(Texture)).Where(s => s.name.Contains(dropdownTextureName)).First();

            if (texture != null)
                return texture;

            return EditorGUIUtility.FindTexture(dropdownTextureName);
        }
    }
}
#endif