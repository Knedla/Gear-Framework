#if UNITY_EDITOR

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GearFramework.Common.EditorOnly
{
    // TODO: make validation to ensure single instance per derived type
    // TODO: add reset to default - there is already something built in - look at Project Settings => Project => Time for example
    public abstract class SettingsObject<TSettings, TSelf> : ScriptableObject where TSettings : class, new() where TSelf : SettingsObject<TSettings, TSelf>
    {
        protected static readonly string rootPath = "Gear Framework";
        static readonly string settingsSuffix = "SettingsObject";
        static readonly string titleLabelClassName = "settings-element-title-label";

        protected static SettingsProvider CreateSettingsProvider()
        {
            SettingsProvider provider = new SettingsProvider(Instance.Path, Instance.SettingsScope, Instance.Keywords)
            {
                activateHandler = (searchContext, rootElement) =>
                {
                    rootElement.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>(PathUtil.GetCallerRelativeFilePath_WithUssFileExtension()));

                    Label title = new Label();
                    title.AddToClassList(titleLabelClassName);
                    title.text = Instance.Title;
                    rootElement.Add(title);

                    SettingsElement settingsElement = Instance.SettingsElement;
                    settingsElement.Init();
                    rootElement.Add(settingsElement);
                }
            };

            return provider;
        }

        static TSelf GetOrCreateInstance()
        {
            TSelf pathInstance = CreateInstance<TSelf>();
            TSelf instance = AssetDatabase.LoadAssetAtPath<TSelf>(PathUtil.GetCallerRelativeFilePath(pathInstance.SettingsAssetFilePath));

            if (instance == null)
                instance = ScriptableObjectUtil.CreateInstanceAndSaveToFileSystem<TSelf>(pathInstance.SettingsAssetFilePath);

            return instance;
        }

        static TSelf instance;
        public static TSelf Instance
        {
            get
             {
                if (instance == null)
                    instance = GetOrCreateInstance();

                return instance;
            }
        }

        protected abstract string SettingsAssetFilePath { get; }
        protected virtual string Path => $"{rootPath}/{Title}";
        protected virtual SettingsScope SettingsScope => SettingsScope.Project;
        protected virtual IEnumerable<string> Keywords
        {
            get
            {
                List<string> keywords = typeof(TSettings).GetFields().Select(s => s.Name).ToList();
                keywords.Add(Title);
                return keywords;
            }
        }
        protected virtual string Title
        {
            get
            {
                string title = GetType().Name;
                return title.Remove(title.Length - settingsSuffix.Length).AddSpacesBeforeCapital();
            }
        }
        protected abstract SettingsElement SettingsElement { get; }

        public TSettings Settings = new TSettings();
    }
}
#endif
