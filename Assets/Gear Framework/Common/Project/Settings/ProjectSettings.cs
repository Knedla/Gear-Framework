#if UNITY_EDITOR

using System;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    [Serializable]
    public class ProjectSettings
    {
        [Header("Project")]
        [NonEditable]
        public string Root;
        [NonEditable]
        public string Instances;
        [NonEditable]
        public string SettingsAssets;

        [Header("Runtime")]
        [NonEditable]
        public string SettingsData = Config.SettingsData;

        public ProjectSettings()
        {
            Root = "Gear Framework";
            Instances = "Instances";
            SettingsAssets = "SettingsAsset";
        }
    }
}
#endif
