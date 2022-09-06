using GearFramework.Common;
using GearFramework.Common.Logger;
using UnityEngine;

/// <summary>
/// Logs messages in either the editor or release
/// </summary>
public static class Console
{
    public static void Log(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.Log(message, messageStyle);
        else
            UnityEngine.Debug.Log(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void Log(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.Log(message, context, messageStyle);
        else
            UnityEngine.Debug.Log(MessageUtil.GetMessage(message, messageStyle), context);
    }
    public static void LogAssert(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.LogAssert(message, messageStyle);
        else
            UnityEngine.Debug.LogAssertion(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void LogAssert(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.LogAssert(message, context, messageStyle);
        else
            UnityEngine.Debug.LogAssertion(MessageUtil.GetMessage(message, messageStyle), context);
    }
    public static void LogError(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.LogError(message, messageStyle);
        else
            UnityEngine.Debug.LogError(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void LogError(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.LogError(message, context, messageStyle);
        else
            UnityEngine.Debug.LogError(MessageUtil.GetMessage(message, messageStyle), context);
    }
    public static void LogException(System.Exception exception)
    {
        if (Gear.Console != null)
            Gear.Console.LogException(exception);
        else
            UnityEngine.Debug.LogException(exception);
    }
    public static void LogException(System.Exception exception, Object context)
    {
        if (Gear.Console != null)
            Gear.Console.LogException(exception, context);
        else
            UnityEngine.Debug.LogException(exception, context);
    }
    public static void LogWarning(object message, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.LogWarning(message, messageStyle);
        else
            UnityEngine.Debug.LogWarning(MessageUtil.GetMessage(message, messageStyle));
    }
    public static void LogWarning(object message, Object context, ITextStyle messageStyle = null)
    {
        if (Gear.Console != null)
            Gear.Console.LogWarning(message, context, messageStyle);
        else
            UnityEngine.Debug.LogWarning(MessageUtil.GetMessage(message, messageStyle), context);
    }
}
