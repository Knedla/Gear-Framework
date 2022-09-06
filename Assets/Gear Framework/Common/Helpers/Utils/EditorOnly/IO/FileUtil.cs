#if UNITY_EDITOR

using System.IO;
using System.Linq;
using UnityEditor;

namespace GearFramework.Common.EditorOnly
{
    public class FileUtil
    {
        private FileUtil() { }

        public static void DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
            File.Delete(PathUtil.AddMetaFileExtension(filePath));
            
            Log(filePath);
            //AssetDatabase.Refresh();
        }

        static void Log(string filePath) => Debug.Log($"{filePath}", MessageStyle.Done("File deleted"));

        public static void CopyFilesMetaExcluded(string sourceDirectoryPath, string targetDirectoryPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourceDirectoryPath, "*", SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourceDirectoryPath, targetDirectoryPath));
            foreach (string newPath in Directory.GetFiles(sourceDirectoryPath, "*.*", SearchOption.AllDirectories).Where(s => !s.EndsWith(".meta")))
                File.Copy(newPath, newPath.Replace(sourceDirectoryPath, targetDirectoryPath), true);

            Log(sourceDirectoryPath, targetDirectoryPath);
            //AssetDatabase.Refresh();
        }

        static void Log(string sourceDirectoryPath, string targetDirectoryPath) => Debug.Log($"{sourceDirectoryPath} to {targetDirectoryPath}", MessageStyle.Done("File copued"));
    }
}
#endif
