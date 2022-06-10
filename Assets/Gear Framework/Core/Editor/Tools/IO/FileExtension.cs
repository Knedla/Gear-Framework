#if UNITY_EDITOR

using System.IO;
using System.Linq;

namespace UnityEditor
{
    public static class FileExtension
    {
        public static bool DeleteFile(string filePath)
        {
            if (!File.Exists(filePath))
                return false;

            File.Delete(filePath);
            File.Delete(PathExtension.AddMetaFileExtension(filePath));

            return true;
        }

        public static void CopyFilesMetaExcluded(string sourceDirectoryPath, string targetDirectoryPath)
        {
            foreach (string dirPath in Directory.GetDirectories(sourceDirectoryPath, Const.Star.ToString(), SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(sourceDirectoryPath, targetDirectoryPath));
            foreach (string newPath in Directory.GetFiles(sourceDirectoryPath, Const.StarDotStar, SearchOption.AllDirectories).Where(s => !s.EndsWith(Const.MetaFileExtensionWithDot)))
                File.Copy(newPath, newPath.Replace(sourceDirectoryPath, targetDirectoryPath), true);
        }
    }
}
#endif
