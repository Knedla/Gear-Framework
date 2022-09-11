#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    class DeleteAllSettingsAssetsDirectories
    {
        [MenuItem("Gear Framework/Delete All SettingsAssets Directories")]
        public static void MenuItem()
        {
            ActionUtil.WrapExecutionWithLog(() => Execute(), "Delete All SettingsAssets Directories");
        }
        
        static void Execute()
        {
            foreach (string item in Directory.GetDirectories(PathUtil.AssetsDirectoryPath, Settings.Project.SettingsAssets, SearchOption.AllDirectories))
                DirectoryUtil.DeleteDirectory(item);
        }
    }
}
#endif
