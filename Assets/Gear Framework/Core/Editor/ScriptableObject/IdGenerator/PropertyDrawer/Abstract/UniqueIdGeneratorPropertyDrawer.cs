#if UNITY_EDITOR

using System;
using System.Reflection;
using UnityEngine;

namespace UnityEditor
{
    abstract class UniqueIdGeneratorPropertyDrawer : PropertyDrawer
    {
        protected abstract SerializedPropertyType SerializedPropertyType { get; }
        bool isValueSet;
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (!isValueSet)
            {
                if (property.propertyType != SerializedPropertyType)
                {
                    ShowWrongFieldTypeErrorMessage(property);
                    return;
                }

                isValueSet = CheckIsValueSet(property);

                if (isValueSet)
                    return;

                isValueSet = true;
                SetValue(property);
            }

            EditorGUI.PropertyField(position, property, label, true);
        }
        void ShowWrongFieldTypeErrorMessage(SerializedProperty property) => Debug.LogError(string.Format("Field type \"{0}\" must be \"{1}\"! {2}", property.name, SerializedPropertyType, property.serializedObject.targetObject.GetType().ToString()));
        bool CheckIsValueSet(SerializedProperty property)
        {
            FieldInfo fieldInfo = GetFieldInfo(property.serializedObject.targetObject.GetType(), property.name);
            
            object value = fieldInfo.GetValue(property.serializedObject.targetObject);
            
            if (value == null)
                return false;
            else
            {
                object defaultValue = fieldInfo.FieldType.IsValueType ? Activator.CreateInstance(fieldInfo.FieldType) : null;

                if (defaultValue == null)
                    return true;
                else
                    return !value.Equals(defaultValue);
            }
        }
        protected FieldInfo GetFieldInfo(Type type, string fieldName)
        {
            FieldInfo fieldInfo = type.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (fieldInfo == null && type.BaseType != null)
                return GetFieldInfo(type.BaseType, fieldName);

            return fieldInfo;
        }
        protected abstract void SetValue(SerializedProperty property);
    }
}
#endif
