#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace UnityEditor
{
    public static class ReflectionExtension
    {
        public static IEnumerable<T> GetSubclassInstancesOf<T>() where T : class
        {
            List<T> objects = new List<T>();

            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
                objects.Add((T)Activator.CreateInstance(type));

            return objects;
        }

        public static List<Type> GeSubclassTypesOf<T>() where T : class
        {
            List<Type> objects = new List<Type>();

            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
                objects.Add(type);

            return objects;
        }

        public static List<string> GetAssetInstanceNames<T>() where T : ScriptableObject
        {
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name);
            List<string> result = new List<string>();

            for (int i = 0; i < guids.Length; i++)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                result.Add(Path.GetFileNameWithoutExtension(path));
            }

            return result;
        }
    }
}
#endif
