#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public class PropertyDrawerElement<T> : SettingsElement where T : ScriptableObject
    {
        T scriptableObject;

        public PropertyDrawerElement(T scriptableObject) => this.scriptableObject = scriptableObject;

        public override void Init()
        {
            SerializedObject serializedObject = new SerializedObject(scriptableObject);
            SerializedProperty serializedProperty = serializedObject.GetIterator();
            serializedProperty.Next(true);

            while (serializedProperty.NextVisible(false))
            {
                PropertyField prop = new PropertyField(serializedProperty);

                // prevent the scripting reference from being drawn
                if (serializedProperty.name == "m_Script")
                    continue;

                prop.Bind(serializedObject);
                Add(prop);
            }
        }
    }
}
#endif
