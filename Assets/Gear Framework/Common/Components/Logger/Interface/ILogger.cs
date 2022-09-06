using UnityEngine;

namespace GearFramework.Common.Logger
{
    public interface ILogger
    {
        void Log(object message, ITextStyle messageStyle = null);
        void Log(object message, Object context, ITextStyle messageStyle = null);
        void LogAssert(object message, ITextStyle messageStyle = null);
        void LogAssert(object message, Object context, ITextStyle messageStyle = null);
        void LogError(object message, ITextStyle messageStyle = null);
        void LogError(object message, Object context, ITextStyle messageStyle = null);
        void LogException(System.Exception exception);
        void LogException(System.Exception exception, Object context);
        void LogWarning(object message, ITextStyle messageStyle = null);
        void LogWarning(object message, Object context, ITextStyle messageStyle = null);
    }
}
