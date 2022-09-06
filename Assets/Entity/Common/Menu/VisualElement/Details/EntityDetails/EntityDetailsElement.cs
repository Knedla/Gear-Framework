#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;
using GearFramework.Common.EditorOnly;
using UnityEditor.UIElements;
using UnityEngine;

namespace Entity
{
    public class EntityDetailsElement : VisualElement
    {
        const string imagesVisualElementName = "images-visual-element";
        const string dataVisualElementName = "data-visual-element";

        VisualElement imagesVisualElement;
        VisualElement dataVisualElement;

        public EntityDetailsElement()
        {
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathUtil.GetCallerRelativeFilePath_WithUxmlFileExtension()).CloneTree(this);
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(PathUtil.GetCallerRelativeFilePath_WithUssFileExtension()));

            imagesVisualElement = this.Q<VisualElement>(imagesVisualElementName);
            dataVisualElement = this.Q<VisualElement>(dataVisualElementName);
        }

        public void SetData(Object obj)
        {
            ClearData();

            if (obj == null)
                return;

            SerializedObject serializedObject = new SerializedObject(obj);
            SerializedProperty serializedProperty = serializedObject.GetIterator();
            serializedProperty.Next(true);

            while (serializedProperty.NextVisible(false))
            {
                PropertyField prop = new PropertyField(serializedProperty);

                prop.SetEnabled(serializedProperty.name != "m_Script");
                prop.Bind(serializedObject);
                dataVisualElement.Add(prop);

                if (serializedProperty.name.Contains("Sprite"))
                {
                    SetImage(out Image image, serializedProperty.name.AddSpacesBeforeCapital());
                    prop.RegisterCallback<ChangeEvent<Object>>(changeEvent => image.sprite = (Sprite)changeEvent.newValue);
                }
            }
        }

        void SetImage(out Image image, string text) // TODO: make ImageDetailsElement control available to change the value
        {
            ImageDetailsElement imageDetailsElement = new ImageDetailsElement(text);
            imagesVisualElement.Add(imageDetailsElement);
            image = imageDetailsElement.Image;
        }

        public void ClearData()
        {
            imagesVisualElement.Clear();
            dataVisualElement.Clear();
        }
    }
}
#endif
