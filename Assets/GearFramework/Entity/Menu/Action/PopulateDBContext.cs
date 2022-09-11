#if UNITY_EDITOR

using GearFramework.Common.EditorOnly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GearFramework.Entity
{
    public class PopulateDBContext
    {
        Button button;
        
        string filePath;
        string assetFilePath;

        GameObject prefabGameObject;
        DbContext dbContext;

        List<ScriptableObject> data;

        public PopulateDBContext(Button button)
        {
            this.button = button;
            button.RegisterCallback<ClickEvent>(clickEvent => Transaction(false, Populate));

            SetPaths();
        }

        void SetPaths()
        {
            filePath = DbContext.GetPrefabFilePath();
            assetFilePath = PathUtil.GetRelativePath(filePath);
        }

        public void SetData(List<ScriptableObject> data)
        {
            this.data = data;

            Transaction(true);
        }

        void Transaction(bool normalizeDbContext, Action action = null)
        {
            LoadPrefab(normalizeDbContext);

            if (action != null)
                action();

            SetEnabled();
            PrefabUtility.UnloadPrefabContents(prefabGameObject);
        }

        void LoadPrefab(bool normalizeDbContext)
        {
            if (File.Exists(filePath))
            {
                LoadPrefabContents();

                if (normalizeDbContext)
                    NormalizeDbContext();
            }
            else
            {
                PrefabUtil.CreatePrefab(filePath, new Type[] { typeof(DbContext) });
                LoadPrefabContents();
            }
        }

        void LoadPrefabContents()
        {
            prefabGameObject = PrefabUtility.LoadPrefabContents(assetFilePath);
            dbContext = prefabGameObject.GetComponent<DbContext>();
        }

        void NormalizeDbContext()
        {
            if (dbContext.Context.Count == 0)
                return;

            int startCount = dbContext.Context.Count;

            dbContext.Context.RemoveAll(s => s == null);

            if (startCount > 1)
                dbContext.Context = dbContext.Context.Distinct().ToList();

            if (startCount != dbContext.Context.Count)
                PrefabUtility.SaveAsPrefabAsset(prefabGameObject, assetFilePath);
        }

        void SetEnabled() => GeneratorHelper.SetEnabled(button, data != null && data.Count > dbContext.Context.Count);

        void Populate()
        {
            dbContext.Context = data;
            PrefabUtility.SaveAsPrefabAsset(prefabGameObject, assetFilePath);
        }

        public void ClearData() => data = null;
    }
}
#endif
