#if UNITY_EDITOR

using System.IO;
using UnityEngine;

namespace UnityEditor
{
    public abstract class BaseBuildAssetBundles
    {
        protected static string GetRootOutputPath(BuildTarget buildTarget) => Path.Combine(Config.AssetBundlesRootOutputPath, buildTarget.ToString());

        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            ExecutionExtension.LogExecuteWrapper(() => BuildAssetBundlesExecute(buildTarget), "BUILD");
        }
        static void BuildAssetBundlesExecute(BuildTarget buildTarget)
        {
            foreach (BaseBuildAssetBundles item in ReflectionExtension.GetSubclassInstancesOf<BaseBuildAssetBundles>())
                ExecutionExtension.LogExecuteWrapper(() => item.Build(buildTarget), item.GetType().ToString());

            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Copy AssetBundles To StreamingAssets")]
        static void CopyAssetBundlesToStreamingAssets() => ExecutionExtension.LogExecuteWrapper(() => CopyAssetBundlesToStreamingAssetsExecute(), "Copy AssetBundles To StreamingAssets");
        static void CopyAssetBundlesToStreamingAssetsExecute()
        {
            DirectoryExtension.ClearDirectory(Application.streamingAssetsPath);
            FileExtension.CopyFilesMetaExcluded(GetRootOutputPath(EditorUserBuildSettings.activeBuildTarget), Application.streamingAssetsPath);
            AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Build AssetBundles And Copy To StreamingAssets")]
        static void BuildAssetBundlesAndCopyToStreamingAssets()
        {
            BuildAssetBundles();
            CopyAssetBundlesToStreamingAssets();
        }

        [MenuItem("Assets/Clear Data")]
        static void ClearData() => ExecutionExtension.LogExecuteWrapper(() => ClearDataExecute(), "Clear");
        static void ClearDataExecute()
        {
            DirectoryExtension.DeleteDirectory(Config.AssetBundlesRootOutputPath);
            DirectoryExtension.DeleteDirectory(Application.streamingAssetsPath);
            AssetDatabase.Refresh();
        }

        protected abstract void Build(BuildTarget buildTarget);
    }
}
#endif
