#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    public abstract class ReferenceDrawer<T> : PropertyDrawer
    {
        static readonly Vector2 dropdownButtonSize = Vector2.one * 20;
        static readonly string dropdownTextureName = "d_icon dropdown";
        static readonly float dropdownFixedWidth = 50;
        static readonly RectOffset dropdownBorder = new RectOffset(1, 1, 1, 1);
        static readonly string constantButtondName = "Constant";
        static readonly string variableButtondName = "Variable";
        static readonly int rightOffset = 15;

        protected static readonly string constantValueFieldName = "constantValue";
        static readonly string useConstantFieldName = "useConstant";
        static readonly string variableFieldName = "variable";

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

        void SetProperty(SerializedProperty property, bool value)
        {
            SerializedProperty propertyRelative = property.FindPropertyRelative(useConstantFieldName);
            propertyRelative.boolValue = value;
            propertyRelative.serializedObject.ApplyModifiedProperties();
        }

        Texture GetTexture()
        {
            Texture texture = (Texture)UnityEngine.Resources.FindObjectsOfTypeAll(typeof(Texture)).Where(s => s.name.Contains(dropdownTextureName)).First();

            if (texture != null)
                return texture;

            return EditorGUIUtility.FindTexture(dropdownTextureName);
        }
    }
}
#endif
