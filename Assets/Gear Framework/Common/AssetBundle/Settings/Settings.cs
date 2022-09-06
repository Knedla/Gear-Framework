#if UNITY_EDITOR

namespace GearFramework.Common.EditorOnly
{
    public partial class Settings
    {
        public static AssetBundleSettings AssetBundle => AssetBundleSettingsObject.Instance.Settings;
    }
}
#endif
