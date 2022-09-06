#if UNITY_EDITOR

using System;

namespace GearFramework.Common.EditorOnly
{
    public class ActionUtil
    {
        private ActionUtil() { }

        public static void WrapExecutionWithLog(Action action, string logMessage)
        {
            Debug.Log($"{logMessage}", MessageStyle.Done($"Action START"));
            action();
            Debug.Log($"{logMessage}", MessageStyle.Done($"Action FINISHED"));
        }
    }
}
#endif
