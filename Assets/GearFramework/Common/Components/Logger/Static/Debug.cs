using GearFramework.Common;
using GearFramework.Common.Logger;
using UnityEngine;

/// <summary>
/// Logs messages, but only when run in the editor
/// </summary>
public static class Debug
{
    public static void Log(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.Log(message, messageStyle);
        else
            UnityEngine.Debug.Log(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void Log(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.Log(message, context, messageStyle);
        else
            UnityEngine.Debug.Log(MessageUtil.GetMessage(message, messageStyle), context);
    }
    public static void LogAssert(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogAssert(message, messageStyle);
        else
            UnityEngine.Debug.LogAssertion(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void LogAssert(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogAssert(message, context, messageStyle);
        else
            UnityEngine.Debug.LogAssertion(MessageUtil.GetMessage(message, messageStyle), context);
    }
    public static void LogError(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogError(message, messageStyle);
        else
            UnityEngine.Debug.LogError(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void LogError(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogError(message, context, messageStyle);
        else
            UnityEngine.Debug.LogError(MessageUtil.GetMessage(message, messageStyle), context);
    }
    public static void LogException(System.Exception exception)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogException(exception);
        else
            UnityEngine.Debug.LogException(exception);
    }
    public static void LogException(System.Exception exception, Object context)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogException(exception, context);
        else
            UnityEngine.Debug.LogException(exception, context);
    }
    public static void LogWarning(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogWarning(message, messageStyle);
        else
            UnityEngine.Debug.LogWarning(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void LogWarning(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Debug != null)
            Gear.Debug.LogWarning(message, context, messageStyle);
        else
            UnityEngine.Debug.LogWarning(MessageUtil.GetMessage(message, messageStyle), context);
    }
}
