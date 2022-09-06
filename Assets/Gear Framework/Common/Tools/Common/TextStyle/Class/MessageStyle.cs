using System;
using UnityEngine;

namespace GearFramework.Common
{
    [Serializable]
    public class MessageStyle : ITextStyle
    {
        public static MessageStyle Done(string prefix) => new MessageStyle(prefix, Color.green);
        public static MessageStyle Log(string prefix) => new MessageStyle(prefix, Color.white);
        public static MessageStyle Assert(string prefix) => new MessageStyle(prefix, Color.red);
        public static MessageStyle Exception(string prefix) => new MessageStyle(prefix, Color.red);
        public static MessageStyle Error(string prefix) => new MessageStyle(prefix, Color.red);
        public static MessageStyle Warning(string prefix) => new MessageStyle(prefix, Color.yellow);

        [SerializeField] string prefix;
        [SerializeField] Color prefixColor;

        public MessageStyle(string prefix, Color prefixColor)
        {
            this.prefix = prefix;
            this.prefixColor = prefixColor;
        }

        public string Apply(object message) => $"<color=#{ColorUtility.ToHtmlStringRGBA(prefixColor)}>{prefix}:</color> {message}";
    }
}
