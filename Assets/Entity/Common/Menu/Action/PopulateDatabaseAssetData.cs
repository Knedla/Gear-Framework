#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine.UIElements;

namespace Entity
{
    public class PopulateDatabaseAssetData
    {
        Button button;

        UnityEngine.Object selectedObject;
        List<Definition.DataEntity> data;
        System.Collections.IDictionary selectedData;
        MethodInfo methodInfo_AddRangeUnsafe;

        public PopulateDatabaseAssetData(Button button)
        {
            this.button = button;
            button.RegisterCallback<ClickEvent>(clickEvent => Populate());
        }

        public void SetData(UnityEngine.Object selectedObject, List<Definition.DataEntity> data)
        {
            this.selectedObject = selectedObject;
            this.data = data;

            if (selectedObject == null || data == null)
            {
                SetEnabled();
                return;
            }
            
            Type type = selectedObject.GetType();
            SetSelectedData(type);

            MethodInfo methodInfo_RemomveNullValues = GetMethodInfo(type, "RemomveNullValues");
            
            if (methodInfo_RemomveNullValues != null)
                methodInfo_RemomveNullValues.Invoke(selectedObject, new object[0]); // thoughts: can it be expensive to execute on every selectedObject change?

            methodInfo_AddRangeUnsafe = GetMethodInfo(type, "AddRangeUnsafe");

            SetEnabled();
        }

        void SetSelectedData(Type type)
        {
            FieldInfo fieldInfo = null;

            while (type != null)
            {
                fieldInfo = type.GetField("data", BindingFlags.Instance | BindingFlags.NonPublic);

                if (fieldInfo != null)
                    break;

                type = type.BaseType;
            }

            if (fieldInfo != null)
                selectedData = (System.Collections.IDictionary)fieldInfo.GetValue(selectedObject);
            else
                GeneratorHelper.LogDatabaseBadType();
        }

        MethodInfo GetMethodInfo(Type type, string name)
        {
            MethodInfo methodInfo = null;

            while (type != null)
            {
                methodInfo = selectedObject.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic);

                if (methodInfo != null)
                    break;

                type = type.BaseType;
            }

            if (methodInfo == null)
                GeneratorHelper.LogDatabaseBadType();

            return methodInfo;
        }

        void SetEnabled() => GeneratorHelper.SetEnabled(button, methodInfo_AddRangeUnsafe != null && selectedData != null && selectedData.Count < data.Count);

        void Populate()
        {
            methodInfo_AddRangeUnsafe.Invoke(selectedObject, new object[] { GetDataToAdd() });

            EditorUtility.SetDirty(selectedObject);
            AssetDatabase.SaveAssetIfDirty(selectedObject);

            SetEnabled();
        }

        System.Collections.IEnumerable GetDataToAdd()
        {
            List<Definition.DataEntity> dataToAdd = new List<Definition.DataEntity>();

            foreach (Definition.DataEntity item in data)
                if (!selectedData.Contains(item.Type))
                    dataToAdd.Add(item);

            return dataToAdd;
        }

        public void ClearData()
        {
            selectedObject = null;
            data = null;
            selectedData = null;
            methodInfo_AddRangeUnsafe = null;
        }
    }
}
#endif
