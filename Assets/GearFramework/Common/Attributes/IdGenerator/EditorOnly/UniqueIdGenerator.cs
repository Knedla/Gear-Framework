#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace GearFramework.Common.EditorOnly
{
    public class UniqueIdGenerator : ScriptableObject
    {
        public static int GenerateId(Type type)
        {
            UniqueIdGenerator instance = GetInstance(type);
            int id = instance.GetNextId();
            EditorUtility.SetDirty(instance);
            AssetDatabase.SaveAssetIfDirty(instance);
            return id;
        }

        static UniqueIdGenerator GetInstance(Type type)
        {
            string[] guids = AssetDatabase.FindAssets($"l:{type.FullName}");

            UniqueIdGenerator generator;

            if (guids.Length == 0)
                generator = ScriptableObjectUtil.CreateInstanceAndSaveToFileSystem<UniqueIdGenerator>(Path.Combine(PathUtil.GetCallerDirectoryPath_ForInstances(), $"{type.FullName}.asset"), new string[] { type.FullName });
            else
                generator = AssetDatabase.LoadAssetAtPath<UniqueIdGenerator>(AssetDatabase.GUIDToAssetPath(guids[0]));

            return generator;
        }

        [Header("Expected Scenario:\nint.MaxValue will never be reached")]
        [NonEditable]
        [SerializeField] int lastAssignedId;
        
        int GetNextId() => ++lastAssignedId;
    }
}
#endif
