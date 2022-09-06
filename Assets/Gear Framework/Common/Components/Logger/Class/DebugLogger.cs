using UnityEngine;

namespace GearFramework.Common.Logger
{
    /// <summary>
    /// Logs messages, but only when run in the editor
    /// </summary>
    public class DebugLogger : BaseLogger
    {
        public DebugLogger(LoggerSettings loggerSettings) : base(loggerSettings) { }

        public override void Log(object message, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.Log(message, messageStyle);
#endif
        }
        public override void Log(object message, Object context, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.Log(message, context, messageStyle);
#endif
        }
        public override void LogAssert(object message, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.LogAssert(message, messageStyle);
#endif
        }
        public override void LogAssert(object message, Object context, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.LogAssert(message, context, messageStyle);
#endif
        }
        public override void LogError(object message, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.LogError(message, messageStyle);
#endif
        }
        public override void LogError(object message, Object context, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.LogError(message, context, messageStyle);
#endif
        }
        public override void LogException(System.Exception exception)
        {
#if UNITY_EDITOR
            base.LogException(exception);
#endif
        }
        public override void LogException(System.Exception exception, Object context)
        {
#if UNITY_EDITOR
            base.LogException(exception, context);
#endif
        }
        public override void LogWarning(object message, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.LogWarning(message, messageStyle);
#endif
        }
        public override void LogWarning(object message, Object context, ITextStyle messageStyle = null)
        {
#if UNITY_EDITOR
            base.LogWarning(message, context, messageStyle);
#endif
        }
    }
}
