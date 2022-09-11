#if UNITY_EDITOR

using UnityEngine.UIElements;

namespace GearFramework.Entity
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

        public static void LogDatabaseBadType() => Debug.LogError("Database.EntityDatabaseAttribute must be applied to a class derived from GearFramework.Common.Database<T> : ScriptableObject where T : ScriptableObject", Common.MessageStyle.Error("Database Generator"));
    }
}
#endif
