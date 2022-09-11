using System;

namespace GearFramework.Common.Logger
{
    [Serializable]
    public abstract class LoggerSettings
    {
        public bool Enabled;
        public bool LogEnabled;
        public bool AssertEnabled;
        public bool ErrorEnabled;
        public bool ExceptionEnabled;
        public bool WarningEnabled;
    }
}
