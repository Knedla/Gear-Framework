#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;
using GearFramework.Common.EditorOnly;

namespace Entity
{
    public class ImageDetailsElement : VisualElement
    {
        const string imageName = "image-details-element-image";
        const string labelName = "image-details-element-label";

        public Image Image;

        public ImageDetailsElement(string text)
        {
            AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathUtil.GetCallerRelativeFilePath_WithUxmlFileExtension()).CloneTree(this);
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(PathUtil.GetCallerRelativeFilePath_WithUssFileExtension()));

            Image = this.Q<Image>(imageName);
            this.Q<Label>(labelName).text = text;
        }
    }
}
#endif
