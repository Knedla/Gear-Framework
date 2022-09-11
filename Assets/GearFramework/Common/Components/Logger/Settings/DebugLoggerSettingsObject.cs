#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public class DebugLoggerSettingsObject : SettingsObject<Logger.DebugSettings, DebugLoggerSettingsObject>
    {
        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider() => CreateSettingsProvider();
        protected override string SettingsAssetFilePath => PathUtil.GetCallerFilePath_ForSettingsAsset();

        protected override string Path => $"{rootPath}/Logger/Debug";
        protected override SettingsElement SettingsElement => new ExtendedSettingsElement(new PropertyDrawerElement<DebugLoggerSettingsObject>(Instance), new HelpBox(helpText, HelpBoxMessageType.Info));

        static readonly string helpText = "Log messages, but only when run in the editor";
    }
}
#endif
