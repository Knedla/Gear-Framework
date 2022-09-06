#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GearFramework.Common.Logger
{
#if UNITY_EDITOR
    [InitializeOnLoad]
#endif
    sealed class Gear
    {
        public static ILogger Debug { get; private set; }
        public static ILogger Console { get; private set; }

#if UNITY_EDITOR
        static Gear() => Init();
#endif

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            Debug = new DebugLogger(GetDebugLoggerSettings());
            Console = new ConsoleLogger(GetConsoleLoggerSettings());
        }

        static LoggerSettings GetDebugLoggerSettings()
        {
#if UNITY_EDITOR
            return EditorOnly.DebugLoggerSettingsObject.Instance.Settings;
#else
            return null;
#endif
        }

        static LoggerSettings GetConsoleLoggerSettings()
        {
#if UNITY_EDITOR
            return EditorOnly.ConsoleLoggerSettingsObject.Instance.Settings;
#else
            return SettingsUtil.GetSettings<ConsoleSettings>();
#endif
        }
    }
}
