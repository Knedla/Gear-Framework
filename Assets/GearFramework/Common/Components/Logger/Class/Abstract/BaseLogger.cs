using UnityEngine;

namespace GearFramework.Common.Logger
{
    public abstract class BaseLogger : ILogger
    {

        protected LoggerSettings loggerSettings;
        public BaseLogger(LoggerSettings loggerSettings) => this.loggerSettings = loggerSettings;

        public virtual void Log(object message, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.LogEnabled)
                UnityEngine.Debug.Log(MessageUtil.GetMessage(message, messageStyle));
        }
        public virtual void Log(object message, Object context, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.LogEnabled)
                UnityEngine.Debug.Log(MessageUtil.GetMessage(message, messageStyle), context);
        }
        public virtual void LogAssert(object message, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.AssertEnabled)
                UnityEngine.Debug.LogAssertion(MessageUtil.GetMessage(message, messageStyle));
        }
        public virtual void LogAssert(object message, Object context, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.AssertEnabled)
                UnityEngine.Debug.LogAssertion(MessageUtil.GetMessage(message, messageStyle), context);
        }
        public virtual void LogError(object message, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.ErrorEnabled)
                UnityEngine.Debug.LogError(MessageUtil.GetMessage(message, messageStyle));
        }
        public virtual void LogError(object message, Object context, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.ErrorEnabled)
                UnityEngine.Debug.LogError(MessageUtil.GetMessage(message, messageStyle), context);
        }
        public virtual void LogException(System.Exception exception)
        {
            if (loggerSettings.Enabled && loggerSettings.ExceptionEnabled)
                UnityEngine.Debug.LogException(exception);
        }
        public virtual void LogException(System.Exception exception, Object context)
        {
            if (loggerSettings.Enabled && loggerSettings.ExceptionEnabled)
                UnityEngine.Debug.LogException(exception, context);
        }
        public virtual void LogWarning(object message, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.WarningEnabled)
                UnityEngine.Debug.LogWarning(MessageUtil.GetMessage(message, messageStyle));
        }
        public virtual void LogWarning(object message, Object context, ITextStyle messageStyle = null)
        {
            if (loggerSettings.Enabled && loggerSettings.WarningEnabled)
                UnityEngine.Debug.LogWarning(MessageUtil.GetMessage(message, messageStyle), context);
        }
    }
}
