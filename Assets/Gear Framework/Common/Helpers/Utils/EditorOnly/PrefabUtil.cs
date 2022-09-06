#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    public class PrefabUtil
    {
        private PrefabUtil() { }

        public static void CreatePrefab(string filePath, Type[] componentTypes)
        {
            DirectoryUtil.CreateDirectory(Path.GetDirectoryName(filePath));
            
            GameObject gameObject = new GameObject();
            foreach (Type item in componentTypes)
                gameObject.AddComponent(item);

            string relativeFilePath = PathUtil.GetRelativePath(filePath);
            PrefabUtility.SaveAsPrefabAsset(gameObject, relativeFilePath);

            UnityEngine.Object.DestroyImmediate(gameObject);

            Debug.Log($"{relativeFilePath}", MessageStyle.Done("Prefab created"));
        }
    }
}
#endif
