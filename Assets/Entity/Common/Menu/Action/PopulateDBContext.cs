#if UNITY_EDITOR

using GearFramework.Common.EditorOnly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Entity
{
    public class PopulateDBContext
    {
        Button button;
        
        string filePath;
        string assetFilePath;

        GameObject prefabGameObject;
        DbContextController dbContext;

        List<ScriptableObject> data;

        public PopulateDBContext(Button button)
        {
            this.button = button;
            button.RegisterCallback<ClickEvent>(clickEvent => Transaction(false, Populate));

            SetPaths();
        }

        void SetPaths()
        {
            filePath = GeneratorHelper.GetDbContextFilePath();
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
                PrefabUtil.CreatePrefab(filePath, new Type[] { typeof(DbContextController) });
                LoadPrefabContents();
            }
        }

        void LoadPrefabContents()
        {
            prefabGameObject = PrefabUtility.LoadPrefabContents(assetFilePath);
            dbContext = prefabGameObject.GetComponent<DbContextController>();
        }

        void NormalizeDbContext()
        {
            if (dbContext.DbContext.Count == 0)
                return;

            int startCount = dbContext.DbContext.Count;

            dbContext.DbContext.RemoveAll(s => s == null);

            if (startCount > 1)
                dbContext.DbContext = dbContext.DbContext.Distinct().ToList();

            if (startCount != dbContext.DbContext.Count)
                PrefabUtility.SaveAsPrefabAsset(prefabGameObject, assetFilePath);
        }

        void SetEnabled() => GeneratorHelper.SetEnabled(button, data != null && data.Count > dbContext.DbContext.Count);

        void Populate()
        {
            dbContext.DbContext = data;
            PrefabUtility.SaveAsPrefabAsset(prefabGameObject, assetFilePath);
        }

        public void ClearData() => data = null;
    }
}
#endif
