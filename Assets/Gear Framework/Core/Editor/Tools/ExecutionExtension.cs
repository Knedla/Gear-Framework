#if UNITY_EDITOR

using System;
using UnityEngine;

namespace UnityEditor
{
    public static class ExecutionExtension
    {
        public static void LogExecuteWrapper(Action action, string logMessage)
        {
            Debug.Log(string.Format("{0} - START", logMessage));
            action();
            Debug.Log(string.Format("{0} - FINISHED", logMessage));
        }
    }
}
#endif
