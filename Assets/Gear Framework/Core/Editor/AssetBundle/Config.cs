﻿#if UNITY_EDITOR

using System.IO;
using UnityEngine;

namespace UnityEditor
{
    public partial class Config
    {
        public readonly static string AssetBundlesRootOutputPath = Path.Combine(PathExtension.ReplaceSlashToBackslash(Application.dataPath), "AssetBundles");
    }
}
#endif
