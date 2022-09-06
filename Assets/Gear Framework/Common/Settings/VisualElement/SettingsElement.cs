#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public abstract class SettingsElement : VisualElement
    {
        new class UxmlTraits : VisualElement.UxmlTraits { }
        protected virtual string UxmlRelativeFilePath => string.Empty;
        protected virtual string UssRelativeFilePath => string.Empty;

        public SettingsElement()
        {
            if (UxmlRelativeFilePath != string.Empty)
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(UxmlRelativeFilePath).CloneTree(this);

            if (UssRelativeFilePath != string.Empty)
                styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(UssRelativeFilePath));
        }

        public abstract void Init();
    }
}
#endif
