#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using GearFramework.Common.EditorOnly;

namespace Entity
{
    public class Generator : EditorWindow
    {
        const string reloadButtonName = "reload-button";
        const string selectDbContextButtonName = "select-dbcontext-button";

        [MenuItem("Entity/Entity Generator")]
        public static void ShowWindow() => GetWindow<Generator>();

        DatabaseGenerator databaseGenerator;
        EntityGenerator entityGenerator;
        EntityDetailsElement entityDetailsElement;

        public void CreateGUI()
        {
            titleContent = new GUIContent("Entity Generator");
            rootVisualElement.Add(AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(PathUtil.GetCallerRelativeFilePath_WithUxmlFileExtension()).Instantiate());
            rootVisualElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(PathUtil.GetCallerRelativeFilePath_WithUssFileExtension()));

            rootVisualElement.Q<Button>(reloadButtonName).RegisterCallback<ClickEvent>(clickEvent => ReloadButton_OnClick(clickEvent));
            rootVisualElement.Q<Button>(selectDbContextButtonName).RegisterCallback<ClickEvent>(clickEvent => ScriptableObjectUtil.SelectAssetFile(AssetDatabase.LoadAssetAtPath<GameObject>(PathUtil.GetRelativePath(GeneratorHelper.GetDbContextFilePath()))));
            
            databaseGenerator = new DatabaseGenerator();
            databaseGenerator.SetData();
            databaseGenerator.OnSelectedObjectValueChangedEvent += DatabaseGenerator_OnSelectedObjectValueChangedEvent;
            
            entityGenerator = new EntityGenerator();
            entityGenerator.SetData();
            entityGenerator.OnSelectedObjectValueChangedEvent += EntityGenerator_OnSelectedObjectValueChangedEvent;

            entityDetailsElement = new EntityDetailsElement();
            rootVisualElement.Add(GetTwoPaneSplitView(databaseGenerator, GetTwoPaneSplitView(entityGenerator, entityDetailsElement)));
        }

        private void DatabaseGenerator_OnSelectedObjectValueChangedEvent(Object selectedObject)
        {
            entityDetailsElement.ClearData();
            entityGenerator.SetData(selectedObject);
        }

        private void EntityGenerator_OnSelectedObjectValueChangedEvent(Object selectedObject) =>  entityDetailsElement.SetData(selectedObject);

        VisualElement GetTwoPaneSplitView(VisualElement firstVisualElement, VisualElement secondVisualElement)
        {
            TwoPaneSplitView twoPaneSplitView = new TwoPaneSplitView(0, 100, TwoPaneSplitViewOrientation.Horizontal);
            twoPaneSplitView.Add(firstVisualElement);
            twoPaneSplitView.Add(secondVisualElement);
            return twoPaneSplitView;
        }

        void ReloadButton_OnClick(ClickEvent clickEvent)
        {
            databaseGenerator.ClearData();
            entityGenerator.ClearData();
            entityDetailsElement.ClearData();

            databaseGenerator.SetData();
            entityGenerator.SetData();
        }

        private void OnDestroy()
        {
            databaseGenerator.OnSelectedObjectValueChangedEvent -= DatabaseGenerator_OnSelectedObjectValueChangedEvent;
            entityGenerator.OnSelectedObjectValueChangedEvent -= EntityGenerator_OnSelectedObjectValueChangedEvent;
        }
    }
}
#endif
