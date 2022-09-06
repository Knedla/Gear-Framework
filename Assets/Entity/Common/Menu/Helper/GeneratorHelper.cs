#if UNITY_EDITOR

using GearFramework.Common.EditorOnly;
using System;
using System.IO;
using UnityEngine.UIElements;

namespace Entity
{
    public class GeneratorHelper
    {
        private GeneratorHelper() { }

        const string buttonActionTrueClassName = "button-action-true";

        public static void SetEnabled(Button button, bool enabled)
        {
            button.SetEnabled(enabled);
            SetButtonAvailability(button);
        }

        static void SetButtonAvailability(Button button)
        {
            if (button.enabledSelf)
                button.AddToClassList(buttonActionTrueClassName);
            else
                button.RemoveFromClassList(buttonActionTrueClassName);
        }

        public static string GetInstancePath(Type type) => PathUtil.GetAssetFilePath_ForInstances(type, PathUtil.GetCallerDirectoryPath(), PathModifyByNamespace.AdjustDirectoryPathWithTypeNamespace);

        public static void LogDatabaseBadType() => Debug.LogError("Database.EntityDatabaseAttribute must be applied to a class derived from GearFramework.Common.Database<T> : ScriptableObject where T : ScriptableObject", GearFramework.Common.MessageStyle.Error("Database Generator"));

        public static string GetDbContextFilePath() => Path.Combine(PathUtil.NavigateBackToTheDirectory(GetInstancePath(typeof(PopulateDBContext)), Settings.Project.Instances), "DbContext.prefab"); // thoughts: overhead?
    }
}
#endif
