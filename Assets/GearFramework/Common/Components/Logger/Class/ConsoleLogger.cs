namespace GearFramework.Common.Logger
{
    /// <summary>
    /// Logs messages in either the editor or release
    /// </summary>
    public class ConsoleLogger : BaseLogger
    {
        public ConsoleLogger(LoggerSettings loggerSettings) : base(loggerSettings) { }
    }
}
