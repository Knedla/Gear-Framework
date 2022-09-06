namespace GearFramework.Common.Logger
{
    public class MessageUtil
    {
        private MessageUtil() { }

        public static object GetMessage(object message, ITextStyle messageStyle = null) => messageStyle != null ? messageStyle.Apply(message) : message;
    }
}