#if UNITY_EDITOR

using GearFramework.Common.EditorOnly;
using System;
using GearFramework.Entity.Definition;
using System.Collections.Generic;

namespace GearFramework.Entity
{
    public class EntityGenerator : GeneratorElement<DataEntity>
    {
        protected override string InstanceLabel => dataType?.FullName;

        PopulateDatabaseAssetData populateDatabaseAssetData;

        UnityEngine.Object databaseObject;
        Type dataType;

        public EntityGenerator() : base("Entity")
        {
            populateDataButton.tooltip = "Populates the Database with all new (missing) entities";
            populateDatabaseAssetData = new PopulateDatabaseAssetData(populateDataButton);
        }

        protected override List<Type> GetAllTypes() => ReflectionUtil.GetSubclassTypesOf(dataType);

        public void SetData(UnityEngine.Object databaseObject)
        {
            ClearData();

            this.databaseObject = databaseObject;

            SetGenericArgument();

            if (dataType == null)
                return;

            SetData();
        }

        void SetGenericArgument()
        {
            Type type = databaseObject.GetType();

            while (type != null)
            {
                if (type.IsGenericType)
                {
                    dataType = type.GetGenericArguments()[0];
                    break;
                }

                type = type.BaseType;
            }

            if (dataType == null)
                GeneratorHelper.LogDatabaseBadType();
        }

        public override void SetData()
        {
            base.SetData();
            populateDatabaseAssetData.SetData(databaseObject, data);
        }

        protected override void GenerateAllAssets()
        {
            base.GenerateAllAssets();
            populateDatabaseAssetData.SetData(databaseObject, data);
        }

        public override void ClearData()
        {
            base.ClearData();
            populateDatabaseAssetData.ClearData();

            databaseObject = null;
            dataType = null;
        }
    }
}
#endif
