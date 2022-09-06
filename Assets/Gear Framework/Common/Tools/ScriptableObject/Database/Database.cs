using System;
using System.Collections.Generic;
using UnityEngine;

namespace GearFramework.Common
{
    public abstract class Database<T> : ScriptableObject where T : ScriptableObject
    {
        public static Database<T> Instance { get; private set; }

        [SerializeField] List<T> items;
        Dictionary<Type, T> data;

        void Awake() => Init();
        void OnEnable() => Init();

        void Init()
        {
            if (Instance != null)
                return;

            Instance = this;
            SetData();
        }

        void SetData()
        {
            data = new Dictionary<Type, T>();

            if (items == null)
                items = new List<T>();

            for (int i = 0; i < items.Count; ++i)
            {
                T item = items[i];

                if (item != null)
                    data.Add(item.GetType(), item);
                else
                {
                    items.RemoveAt(i); // #autoremove deleted items 
                    i--;
                }
            }
        }

        public T GetData(Type dataType)
        {
            T value;
            if (!data.TryGetValue(dataType, out value))
            {
                LogKeyNotExists(dataType);
                return null;
            }

            return value;
        }

        public U GetData<U>() where U : ScriptableObject
        {
            T value;
            if (!data.TryGetValue(typeof(U), out value))
            {
                LogKeyNotExists(typeof(U));
                return null;
            }

            return value as U;
        }

        void LogKeyNotExists(Type dataType) => Debug.Log($"The given key was not present in the Database: {dataType.FullName}", MessageStyle.Error($"Database: {typeof(T).FullName}"));

#if UNITY_EDITOR // note: those methods are called via reflection
        protected void RemomveNullValues() 
        {
            int removedCount = items.RemoveAll(s => s == null);

            if (removedCount == 0)
                return;

            data.Clear();
            items.ForEach(s => data.Add(s.GetType(), s));
        }
        protected void AddRangeUnsafe(System.Collections.IEnumerable items)
        {
            foreach (T item in items)
            {
                this.items.Add(item);
                data.Add(item.GetType(), item);
            }
        }
#endif
    }
}
