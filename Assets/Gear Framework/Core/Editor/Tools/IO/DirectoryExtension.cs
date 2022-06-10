#if UNITY_EDITOR

using System.IO;
using UnityEngine;

namespace UnityEditor
{
    public static class DirectoryExtension
    {
        public static bool DeleteDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                return false;

            Directory.Delete(directoryPath, true);
            File.Delete(PathExtension.AddMetaFileExtension(directoryPath));

            return true;
        }

        public static void ClearDirectory(string directoryPath)
        {
            DeleteDirectory(directoryPath);
            Directory.CreateDirectory(directoryPath);
        }

        public static void CreateDirectoryIfDoesNotExists(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
                Debug.Log(string.Format("Directory \"{0}\" created!", directoryPath));
            }
        }
    }
}
#endif
