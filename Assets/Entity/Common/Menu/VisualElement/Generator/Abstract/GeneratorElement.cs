#if UNITY_EDITOR

using UnityEditor;
using UnityEngine.UIElements;
using GearFramework.Common.EditorOnly;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Entity
{
    public abstract class GeneratorElement<T> : VisualElement where T : ScriptableObject
    {
        // quickfix #1 - after executing the command 'twoPaneSplitView.CollapseChild(1)' the scroll stops appearing

        const string titleLabelName = "title-label";
        const string generateAllAssetsButtonName = "generate-all-assets-button";
        const string populateDataButtonName = "populate-data-button";
        const string selectAssetButtonName = "select-asset-button";

        const string greenTextClass = "green-text";
        const string centerDataClass = "center-data";

        public delegate void OnSelectedObjectValueChanged(UnityEngine.Object selectedObject);
        public event OnSelectedObjectValueChanged OnSelectedObjectValueChangedEvent;

        Button generateAllAssetsButton;
        protected Button populateDataButton;

        TwoPaneSplitView twoPaneSplitView;
        ListView dataListView;
        ListView newDataListView;
        Label newLabel;

        HashSet<Type> dataTypes;
        protected List<T> data;
        List<T> newData;
        List<Type> newDataTypes;

        UnityEngine.Object selectedObject;

        protected abstract string InstanceLabel { get; }

        string treamItemNameBy;

        public GeneratorElement(string title, string treamItemNameBy = null)
        {
            this.treamItemNameBy = treamItemNameBy;

            InitData();
            SetView(title);
        }

        void InitData()
        {
            dataTypes = new HashSet<Type>();
            data = new List<T>();
            newData = new List<T>();
            newDataTypes = new List<Type>();
        }

        void SetView(string title)
        {
            Add(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathUtil.GetCallerRelativeFilePath_WithUxmlFileExtension()).Instantiate());
            styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(PathUtil.GetCallerRelativeFilePath_WithUssFileExtension()));

            this.Q<Label>(titleLabelName).text = title;
            generateAllAssetsButton = this.Q<Button>(generateAllAssetsButtonName);
            generateAllAssetsButton.RegisterCallback<ClickEvent>(clickEvent => GenerateAllAssets());
            populateDataButton = this.Q<Button>(populateDataButtonName);
            this.Q<Button>(selectAssetButtonName).RegisterCallback<ClickEvent>(clickEvent => ScriptableObjectUtil.SelectAssetFile(selectedObject));

            dataListView = GetListViewStyle();
            newDataListView = GetListViewStyle();

            VisualElement visualElement = new VisualElement();
            visualElement.Add(GetLabel());
            visualElement.Add(newDataListView);

            twoPaneSplitView = new TwoPaneSplitView(0, 300, TwoPaneSplitViewOrientation.Vertical);
            twoPaneSplitView.Add(dataListView);
            twoPaneSplitView.Add(visualElement);

            Add(twoPaneSplitView);
        }

        ListView GetListViewStyle()
        {
            ListView listView = new ListView();
            listView.fixedItemHeight = 16;
            listView.selectionType = SelectionType.Single;
            listView.AddToClassList(centerDataClass);
            return listView;
        }

        Label GetLabel()
        {
            newLabel = new Label("New");
            newLabel.AddToClassList(greenTextClass);
            newLabel.AddToClassList(centerDataClass);
            return newLabel;
        }

        public virtual void SetData()
        {
            LoadData();
            SetButtons();
            PopulateDataListView(dataListView, data);
            RecalculateDataTwoPaneSplitView();
        }

        void LoadData()
        {
            if (InstanceLabel == null)
                return;

            string[] guids = AssetDatabase.FindAssets($"l:{InstanceLabel}");
            for (int i = 0; i < guids.Length; i++)
            {
                T instance = AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(guids[i]));
                dataTypes.Add(instance.GetType());
                data.Add(instance);
            }

            foreach (Type type in GetAllTypes())
                if (!dataTypes.Contains(type))
                    newDataTypes.Add(type);
        }

        protected abstract List<Type> GetAllTypes();

        void SetButtons() => GeneratorHelper.SetEnabled(generateAllAssetsButton, newDataTypes.Count > 0);

        void PopulateDataListView(ListView listView, List<T> items)
        {
            listView.makeItem = () => new Label();
            listView.bindItem = (element, i) => (element as Label).text = ((treamItemNameBy == null) ? items[i].name : items[i].name.Replace(treamItemNameBy, "")).AddSpacesBeforeCapital();
            listView.itemsSource = items;
            listView.selectedIndex = -1;
            listView.onSelectionChange += (enumerable) =>
            {
                foreach (UnityEngine.Object unityObject in enumerable)
                {
                    if (selectedObject == unityObject)
                        return;

                    selectedObject = unityObject;
                    OnSelectedObjectValueChangedEvent?.Invoke(selectedObject);
                }
            };

            listView.Rebuild();
        }

        void RecalculateDataTwoPaneSplitView()
        {
            #region quickfix #1: delete region after resolving
            PopulateDataListView(newDataListView, newData);

            if (newData.Count > 0)
                newLabel.AddToClassList(greenTextClass);
            else
                newLabel.RemoveFromClassList(greenTextClass);
            #endregion

            #region quickfix #1: uncomment region after resolving
            //if (newData.Count > 0)
            //{
            //    twoPaneSplitView.UnCollapse();
            //    PopulateDataListView(newDataListView, newData);
            //}
            //else
            //    twoPaneSplitView.CollapseChild(1);
            #endregion
        }

        public virtual void ClearData()
        {
            ClearListView(dataListView);
            ClearListView(newDataListView);

            dataTypes.Clear();
            data.Clear();
            newData.Clear();
            newDataTypes.Clear();

            selectedObject = null;
        }

        void ClearListView(ListView listView)
        {
            listView.itemsSource = null;
            listView.Rebuild();
        }

        protected virtual void GenerateAllAssets()
        {
            foreach (Type type in newDataTypes)
            {
                string filePath = GeneratorHelper.GetInstancePath(type);
                if (filePath == null)
                    continue;

                T instance = (T)ScriptableObjectUtil.CreateInstanceAndSaveToFileSystem(type, filePath, new string[] { InstanceLabel });
                dataTypes.Add(instance.GetType());
                data.Add(instance);
                newData.Add(instance);
            }

            newDataTypes.Clear();
            dataListView.Rebuild(); // workaround: must be executed to remove the "List is empty" label if it is active
            SetButtons();
            RecalculateDataTwoPaneSplitView();
        }
    }
}
#endif
