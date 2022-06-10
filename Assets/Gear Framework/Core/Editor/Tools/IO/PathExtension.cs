#if UNITY_EDITOR

using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityEditor
{
    public static class PathExtension
    {
        public static string ReplaceSlashToBackslash(string path) => path.Replace(Const.Slash, Const.Backslash);

        public static string GetCallerFilePath([CallerFilePath] string filePath = null) => filePath; // get method caller full directory path with name and extension
        public static string GetCallerDirectoryPath([CallerFilePath] string filePath = null) => Path.GetDirectoryName(filePath); // get method caller full directory path
        public static string GetCallerRelativeFilePath([CallerFilePath] string filePath = null) => GetRelativePath(filePath);

        public static string GetRelativePath(string path) => ReplaceSlashToBackslash(path).Replace(string.Concat(Path.GetDirectoryName(Application.dataPath), Const.Backslash), string.Empty);

        public static string AddMetaFileExtension(string path) => string.Concat(path, Const.MetaFileExtensionWithDot);
        public static string AddAssetFileExtension(string path) => string.Concat(path, Const.AssetFileExtensionWithDot);
        public static string ChangeFileExtensionToUxml(string path) => Path.ChangeExtension(path, Const.UxmlFileExtension);
    }
}
#endif
