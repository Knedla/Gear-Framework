#if UNITY_EDITOR

using System;

namespace GearFramework.Common.EditorOnly
{
    [Serializable]
    public class AssetBundleSettings
    {
        [NonEditable]
        public string OutputDirectoryName;

        public AssetBundleSettings() => OutputDirectoryName = "AssetBundles";
    }
}
#endif
