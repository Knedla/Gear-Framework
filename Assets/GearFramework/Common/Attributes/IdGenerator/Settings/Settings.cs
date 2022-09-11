#if UNITY_EDITOR

namespace GearFramework.Common.EditorOnly
{
    partial class Settings
    {
        public static IdGeneratorSettings IdGenerator => IdGeneratorSettingsObject.Instance.Settings;
    }
}
#endif
