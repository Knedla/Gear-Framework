#if UNITY_EDITOR

using GearFramework.Common.EditorOnly;
using UnityEditor;
using UnityEngine.UIElements;

namespace GearFramework.LogRecorder.EditorOnly
{
    public class LogRecorderSettingsObject : SettingsObject<Settings, LogRecorderSettingsObject>
    {
        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider() => CreateSettingsProvider();
        protected override string SettingsAssetFilePath => PathUtil.GetCallerFilePath_ForSettingsAsset();

        protected override SettingsElement SettingsElement => new ExtendedSettingsElement(new PropertyDrawerElement<LogRecorderSettingsObject>(Instance), new HelpBox(helpText, HelpBoxMessageType.Info), GetOpenDirectoryLocationButton());

        Button GetOpenDirectoryLocationButton()
        {
            Button button = new Button();
            button.text = "Open Directory Location";
            button.RegisterCallback<ClickEvent>(evt => OpenDirectoryLocationButton_OnClick(evt));
            return button;
        }

        void OpenDirectoryLocationButton_OnClick(ClickEvent evt)
        {
            DirectoryUtil.CreateDirectory(Gear.Manager.DirectoryPath);
            EditorUtility.RevealInFinder(Gear.Manager.DirectoryPath);
        }

        static readonly string helpText = "Log messages to file" +
            "\nThese settings are loaded for the editor only." +
            "\nFor release they are loaded from another location.";
    }
}
#endif
