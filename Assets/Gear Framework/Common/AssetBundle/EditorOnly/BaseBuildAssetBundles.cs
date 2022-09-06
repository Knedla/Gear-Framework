#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    public abstract class BaseBuildAssetBundles
    {
        static string outputDirectoryPath = Path.Combine(PathUtil.AssetsDirectoryPath.Replace('/', '\\'), Settings.AssetBundle.OutputDirectoryName);
        protected static string GetRootOutputPath(BuildTarget buildTarget) => Path.Combine(outputDirectoryPath, buildTarget.ToString());

        [MenuItem("Assets/Build AssetBundles")]
        static void BuildAssetBundles()
        {
            BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
            ActionUtil.WrapExecutionWithLog(() => BuildAssetBundlesExecute(buildTarget), "BUILD Asset Bundles");
        }
        static void BuildAssetBundlesExecute(BuildTarget buildTarget)
        {
            foreach (BaseBuildAssetBundles item in ReflectionUtil.GetSubclassInstancesOf<BaseBuildAssetBundles>())
                ActionUtil.WrapExecutionWithLog(() => item.Build(buildTarget), item.GetType().ToString());

            //AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Copy AssetBundles To StreamingAssets")]
        static void CopyAssetBundlesToStreamingAssets() => ActionUtil.WrapExecutionWithLog(() => CopyAssetBundlesToStreamingAssetsExecute(), "Copy AssetBundles To StreamingAssets");
        static void CopyAssetBundlesToStreamingAssetsExecute()
        {
            DirectoryUtil.ClearDirectory(PathUtil.StreamingAssetsDirectoryPath);
            FileUtil.CopyFilesMetaExcluded(GetRootOutputPath(EditorUserBuildSettings.activeBuildTarget), PathUtil.StreamingAssetsDirectoryPath);
            //AssetDatabase.Refresh();
        }

        [MenuItem("Assets/Build AssetBundles And Copy To StreamingAssets")]
        static void BuildAssetBundlesAndCopyToStreamingAssets()
        {
            BuildAssetBundles();
            CopyAssetBundlesToStreamingAssets();
        }

        [MenuItem("Assets/Clear Data")]
        static void ClearData() => ActionUtil.WrapExecutionWithLog(() => ClearDataExecute(), "Clear");
        static void ClearDataExecute()
        {
            DirectoryUtil.DeleteDirectory(outputDirectoryPath);
            DirectoryUtil.DeleteDirectory(PathUtil.StreamingAssetsDirectoryPath);
            //AssetDatabase.Refresh();
        }

        protected abstract void Build(BuildTarget buildTarget);
    }
}
#endif
