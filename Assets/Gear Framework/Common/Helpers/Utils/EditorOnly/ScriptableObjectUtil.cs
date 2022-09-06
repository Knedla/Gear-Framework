#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    public class ScriptableObjectUtil
    {
        private ScriptableObjectUtil() { }

        public static ScriptableObject CreateInstanceAndSaveToFileSystem(string className, string assetFilePath, string[] labels = null)
        {
            ScriptableObject instance = ScriptableObject.CreateInstance(className);
            SaveAsset(instance, assetFilePath, labels);
            return instance;
        }

        public static ScriptableObject CreateInstanceAndSaveToFileSystem(Type type, string assetFilePath, string[] labels = null)
        {
            ScriptableObject instance = ScriptableObject.CreateInstance(type);
            SaveAsset(instance, assetFilePath, labels);
            return instance;
        }

        public static T CreateInstanceAndSaveToFileSystem<T>(string assetFilePath, string[] labels = null) where T : ScriptableObject
        {
            T instance = ScriptableObject.CreateInstance<T>();
            SaveAsset(instance, assetFilePath, labels);
            return instance;
        }

        static void SaveAsset(ScriptableObject instance, string assetFilePath, string[] labels = null)
        {
            DirectoryUtil.CreateDirectory(Path.GetDirectoryName(assetFilePath));

            string assetRelativeFilePath = PathUtil.GetCallerRelativeFilePath(assetFilePath);
            AssetDatabase.CreateAsset(instance, assetRelativeFilePath);

            if (labels != null)
                AssetDatabase.SetLabels(instance, labels);

            Log(assetRelativeFilePath);
            AssetDatabase.SaveAssets();
            //AssetDatabase.Refresh();
        }

        static void Log(string assetRelativeFilePath) => Debug.Log($"{assetRelativeFilePath}", MessageStyle.Done("Asset created"));

        public static List<string> GetAssetInstanceNames<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            List<string> result = new List<string>();

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                result.Add(Path.GetFileNameWithoutExtension(path));
            }

            return result;
        }

        public static List<T> GetAssetInstances<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{typeof(T).Name}");
            List<T> result = new List<T>();

            for (int i = 0; i < guids.Length; i++)
                result.Add(AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i])));

            return result;
        }

        public static T GetAssetInstance<T>(Type type) where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets($"t:{type.Name}");

            for (int i = 0; i < guids.Length; i++)
            {
                T result = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
                if (type.IsInstanceOfType(result))
                    return result;
            }

            return null;
        }

        public static void SelectAssetFile(UnityEngine.Object obj)
        {
            if (obj != null)
                Selection.activeObject = AssetDatabase.LoadAssetAtPath(AssetDatabase.GetAssetPath(obj), obj.GetType());
            else
                Debug.LogWarning($"Input value is null", MessageStyle.Warning("Select Asset File"));
        }
    }
}
#endif
