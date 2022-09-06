#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    public class PathUtil
    {
        private PathUtil() { }

        public static string AssetsDirectoryPath => Application.dataPath;
        public static string StreamingAssetsDirectoryPath => Application.streamingAssetsPath;

        /// <summary>
        /// Inserts a dedicated instance directory after the GearFramework root directory, while keeping the rest of the path intact
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetCallerFilePath_ForInstances([CallerFilePath] string filePath = null) => GetPath_ForInstances(filePath, Settings.Project.Root);
        /// <summary>
        /// Inserts a dedicated instance directory after the GearFramework root directory, while keeping the rest of the path intact
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetCallerDirectoryPath_ForInstances([CallerFilePath] string filePath = null) => GetPath_ForInstances(Path.GetDirectoryName(filePath), Settings.Project.Root);
        /// <summary>
        /// Inserts the instance's dedicated directory after the passed directory, while keeping the rest of the path intact
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="insertAfterDirectory"></param>
        /// <returns></returns>
        public static string GetPath_ForInstances(string path, string insertAfterDirectory)
        {
            List<string> directories = new List<string>(path.Split('\\'));
            directories.Reverse();

            for (int i = 0; i < directories.Count; i++)
                if (directories[i] == insertAfterDirectory)
                {
                    directories.Insert(i, Settings.Project.Instances);
                    break;
                }

            directories.Reverse();
            return directories.Aggregate((a, b) => a + '\\' + b);
        }

        public static string NavigateBackToTheDirectory(string path, string directory, bool removeDirectoryAsWell = false)
        {
            List<string> directories = new List<string>(path.Split('\\'));

            for (int i = directories.Count - 1; i > 0; i--)
            {
                string currentDirectory = directories[i];
                if (currentDirectory == directory)
                {
                    if (removeDirectoryAsWell)
                        directories.Remove(currentDirectory);
                    break;
                }
                else
                    directories.Remove(currentDirectory);
            }

            return directories.Aggregate((a, b) => a + '\\' + b);
        }

        public static string GetCallerFilePath([CallerFilePath] string filePath = null) => filePath; // get method caller full directory path with name and extension
        public static string GetCallerDirectoryPath([CallerFilePath] string filePath = null) => Path.GetDirectoryName(filePath); // get method caller full directory path
        public static string GetCallerRelativeFilePath([CallerFilePath] string filePath = null) => GetRelativePath(filePath);

        public static string GetCallerFilePath_ForSettingsAsset([CallerFilePath] string filePath = null) => Path.Combine(Path.GetDirectoryName(filePath), Settings.Project.SettingsAssets, AddAssetFileExtension(Path.GetFileNameWithoutExtension(filePath)));
        
        public static string GetRelativePath(string path) => path.Replace('/', '\\').Replace($"{Path.GetDirectoryName(AssetsDirectoryPath)}\\", string.Empty);

        public static string AddMetaFileExtension(string path) => $"{path}.meta";
        public static string AddAssetFileExtension(string path) => $"{path}.asset";
        
        public static string GetCallerRelativeFilePath_WithUxmlFileExtension([CallerFilePath] string filePath = null) => Path.ChangeExtension(GetRelativePath(filePath), "uxml");
        public static string GetCallerRelativeFilePath_WithUssFileExtension([CallerFilePath] string filePath = null) => Path.ChangeExtension(GetRelativePath(filePath), "uss");

        public static string GetAssetFilePath_ForInstances(Type type, string searchDirectoryPath, PathModifyByNamespace pathModifyByNamespace = PathModifyByNamespace.NaN)
        {
            string rootDirectoryName = type.Namespace.Split('.')[0];
            string namespaceToPathPart = type.Namespace.Replace(".", "\\");

            if (pathModifyByNamespace == PathModifyByNamespace.AdjustDirectoryPathToRootOfTypeNamespace)
                searchDirectoryPath = NavigateBackToTheDirectory(searchDirectoryPath, rootDirectoryName);
            else if (pathModifyByNamespace == PathModifyByNamespace.AdjustDirectoryPathWithTypeNamespace)
            {
                string[] path = type.Namespace.Split('.');
                path[0] = NavigateBackToTheDirectory(searchDirectoryPath, rootDirectoryName);
                searchDirectoryPath = Path.Combine(path);
            }

            string[] allfiles = Directory.GetFiles(searchDirectoryPath, $"{type.Name}.cs", SearchOption.AllDirectories);
            foreach (string item in allfiles)
            {
                if (item.Contains(namespaceToPathPart))
                    return GetAssetFilePath_ForInstances(item, rootDirectoryName);
            }

            if (allfiles.Length > 0)
            {
                Debug.LogWarning($"The system is not sure about the type '{type.FullName}' if the path is correct:\n'{allfiles[0]}'.\n" +
                    $"If this is not the correct path, the asset was generated in the wrong place and should be manually moved to where other related assets are located.\n", MessageStyle.Warning("Path Util"));

                return GetAssetFilePath_ForInstances(allfiles[0], rootDirectoryName);
            }

            Debug.LogError($"A concrete class of type '{type.FullName}' does not exists.", MessageStyle.Error("Path Util"));

            return null;
        }

        static string GetAssetFilePath_ForInstances(string filePath, string insertAfterDirectory) => Path.ChangeExtension(GetPath_ForInstances(filePath, insertAfterDirectory), "asset");
    }
}
#endif
