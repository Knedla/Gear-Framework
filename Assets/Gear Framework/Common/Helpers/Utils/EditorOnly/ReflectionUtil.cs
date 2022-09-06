#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GearFramework.Common.EditorOnly
{
    public class ReflectionUtil
    {
        private ReflectionUtil() { }

        public static List<T> GetSubclassInstancesOf<T>() where T : class
        {
            List<T> objects = new List<T>();

            foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(T))))
                objects.Add((T)Activator.CreateInstance(type));

            return objects;
        }

        public static List<Type> GetDirectDerivedTypesOf<T>() where T : class => Assembly.GetAssembly(typeof(T)).GetTypes().Where(t => t.BaseType == typeof(T)).ToList();

        public static List<Type> GetSubclassTypesOf<T>() where T : class => GetSubclassTypesOf(typeof(T));
        public static List<Type> GetSubclassTypesOf(Type type) => Assembly.GetAssembly(type).GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(type)).ToList();
        
        public static List<Type> GetTypesWithAttribute<T>() where T : Attribute => Assembly.GetAssembly(typeof(T)).GetTypes().Where(t => t.GetCustomAttributes(typeof(T), true).Length > 0).ToList();
    }
}
#endif
