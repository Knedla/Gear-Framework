#if UNITY_EDITOR

using System;
using System.IO;
using UnityEngine;

namespace UnityEditor
{
    public class UniqueIdGenerator : ScriptableObject
    {
        static string GetFileNameWithoutExtension(Type type) => type.ToString();

        public static int GenerateId(Type type) => GetInstance(type).GetNextId();
        static UniqueIdGenerator GetInstance(Type type)
        {
            UniqueIdGenerator generator;

            string[] guids = AssetDatabase.FindAssets(string.Concat("l:", GetFileNameWithoutExtension(type)), new[] { PathExtension.GetRelativePath(Config.UniqueIdGeneratorDirectoryPath) });

            if (guids.Length == 0)
                generator = CreateAsset(type);
            else
                generator = AssetDatabase.LoadAssetAtPath<UniqueIdGenerator>(AssetDatabase.GUIDToAssetPath(guids[0]));

            EditorUtility.SetDirty(generator);
            return generator;
        }
        static UniqueIdGenerator CreateAsset(Type type)
        {
            DirectoryExtension.CreateDirectoryIfDoesNotExists(Config.UniqueIdGeneratorDirectoryPath);

            UniqueIdGenerator generator = CreateInstance<UniqueIdGenerator>();
            string filePath = Path.Combine(PathExtension.GetRelativePath(Config.UniqueIdGeneratorDirectoryPath), string.Concat(GetFileNameWithoutExtension(type), Const.AssetFileExtensionWithDot));

            AssetDatabase.CreateAsset(generator, filePath);
            AssetDatabase.SetLabels(generator, new string[] { GetFileNameWithoutExtension(type) });
            Debug.Log(string.Format("Generator \"{0}\" created!", filePath));

            return generator;
        }

        [NonEditable]
        [SerializeField] int lastAssignedId; // expected scenario: int.MaxValue will never be used
        int GetNextId() => ++lastAssignedId;
    }
}
#endif
