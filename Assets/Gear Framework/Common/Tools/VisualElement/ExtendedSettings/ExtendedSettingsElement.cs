#if UNITY_EDITOR

using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public class ExtendedSettingsElement : SettingsElement
    {
        SettingsElement settingsElement;
        VisualElement prefixVisualElement;
        VisualElement suffixVisualElement;

        public ExtendedSettingsElement(SettingsElement settingsElement, VisualElement prefixVisualElement = null, VisualElement suffixVisualElement = null)
        {
            this.settingsElement = settingsElement;
            this.prefixVisualElement = prefixVisualElement;
            this.suffixVisualElement = suffixVisualElement;
        }

        public override void Init()
        {
            if (prefixVisualElement != null)
                Add(prefixVisualElement);

            settingsElement.Init();
            Add(settingsElement);

            if (suffixVisualElement != null)
                Add(suffixVisualElement);
        }
    }
}
#endif
