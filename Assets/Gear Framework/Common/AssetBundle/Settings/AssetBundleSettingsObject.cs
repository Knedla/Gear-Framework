#if UNITY_EDITOR

using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    public class AssetBundleSettingsObject : SettingsObject<AssetBundleSettings, AssetBundleSettingsObject>
    {
        [SettingsProvider]
        static SettingsProvider RegisterSettingsProvider() => CreateSettingsProvider();
        protected override string SettingsAssetFilePath => PathUtil.GetCallerFilePath_ForSettingsAsset();

        protected override string Title => "Asset Bundles";
        protected override SettingsElement SettingsElement => new PropertyDrawerElement<AssetBundleSettingsObject>(Instance);
    }
}
#endif
