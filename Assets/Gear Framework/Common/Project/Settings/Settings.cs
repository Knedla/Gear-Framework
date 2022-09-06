#if UNITY_EDITOR

namespace GearFramework.Common.EditorOnly
{
    partial class Settings
    {
        static ProjectSettings project = new ProjectSettings();
        public static ProjectSettings Project => project;
    }
}
#endif
