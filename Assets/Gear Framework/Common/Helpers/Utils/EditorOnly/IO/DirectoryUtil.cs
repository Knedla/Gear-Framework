#if UNITY_EDITOR

using System.IO;
using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    public class DirectoryUtil
    {
        private DirectoryUtil() { }

        public static void DeleteDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return;

            Directory.Delete(directoryPath, true);
            File.Delete(PathUtil.AddMetaFileExtension(directoryPath));
            
            Log(directoryPath, "deleted");
            //AssetDatabase.Refresh();
        }

        public static void CreateDirectory(string directoryPath)
        {
            if (Directory.Exists(directoryPath))
                return;

            Directory.CreateDirectory(directoryPath);
            
            Log(directoryPath, "created");
            //AssetDatabase.Refresh();
        }

        public static void ClearDirectory(string directoryPath)
        {
            DeleteDirectory(directoryPath);
            CreateDirectory(directoryPath);

            Log(directoryPath, "cleared");
        }

        static void Log(string directoryPath, string action) => Debug.Log($"{directoryPath}", MessageStyle.Done($"Directory {action}"));
    }
}
#endif
