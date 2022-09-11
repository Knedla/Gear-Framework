using System;

namespace GearFramework.Common.Logger
{
    [Serializable]
    public class ConsoleSettings : LoggerSettings
    {
        public ConsoleSettings()
        {
            Enabled = true;
            LogEnabled = true;
            AssertEnabled = false;
            ErrorEnabled = true;
            ExceptionEnabled = true;
            WarningEnabled = true;
        }
    }
}
