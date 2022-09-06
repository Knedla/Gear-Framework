#if UNITY_EDITOR

using UnityEngine;
using GearFramework.Common.EditorOnly;
using System;
using System.Collections.Generic;

namespace Entity
{
    public class DatabaseGenerator : GeneratorElement<ScriptableObject>
    {
        string instanceLabel = typeof(Database.EntityDatabaseAttribute).FullName;
        protected override string InstanceLabel => instanceLabel;

        PopulateDBContext populateDBContext;

        public DatabaseGenerator() : base("Database", "Database")
        {
            populateDataButton.tooltip = "Populates the DB Context with all new (missing) databases";
            populateDBContext = new PopulateDBContext(populateDataButton);
        }

        protected override List<Type> GetAllTypes() => ReflectionUtil.GetTypesWithAttribute<Database.EntityDatabaseAttribute>();

        public override void SetData()
        {
            base.SetData();
            populateDBContext.SetData(data);
        }

        protected override void GenerateAllAssets()
        {
            base.GenerateAllAssets();
            populateDBContext.SetData(data);
        }

        public override void ClearData()
        {
            base.ClearData();
            populateDBContext.ClearData();
        }
    }
}
#endif
