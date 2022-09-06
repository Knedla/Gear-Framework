using GearFramework.Common;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GearFramework.LogRecorder
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    sealed class Gear
    {
        public static IManager Manager { get; private set; }

#if UNITY_EDITOR
        static Gear() => Init();
#endif

        [RuntimeInitializeOnLoadMethod]
        static void Init() => Manager = new Manager(GetLogRecorderSettings(), new TimeBasedNameGenerator());

        static Settings GetLogRecorderSettings()
        {
#if UNITY_EDITOR
            return EditorOnly.LogRecorderSettingsObject.Instance.Settings;
#else
            return SettingsUtil.GetSettings<Settings>();
#endif
        }
    }
}
