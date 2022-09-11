#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    public class ConsoleLoggerSettingsObject : SettingsObject<Logger.ConsoleSettings, ConsoleLoggerSettingsObject>
    {
        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider() => CreateSettingsProvider();
        protected override string SettingsAssetFilePath => PathUtil.GetCallerFilePath_ForSettingsAsset();

        protected override string Path => $"{rootPath}/Logger/Console";
        protected override SettingsElement SettingsElement => new ExtendedSettingsElement(new PropertyDrawerElement<ConsoleLoggerSettingsObject>(Instance), new HelpBox(helpText, HelpBoxMessageType.Info));

        static readonly string helpText = "Log messages in either the editor or release"
            + "\nThese settings are loaded for the editor only."
            + "\nFor release they are loaded from another location.";
    }
}
#endif
