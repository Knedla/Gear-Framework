using System;
using UnityEngine;

namespace GearFramework.LogRecorder
{
    [Serializable]
    public class Log
    {
        public string Condition;
        public string StackTrace;
        public LogType Type;
        public string DateTime;

        public Log(string condition, string stackTrace, LogType type, string dateTime)
        {
            Condition = condition;
            StackTrace = stackTrace;
            Type = type;
            DateTime = dateTime;
        }
    }
}
