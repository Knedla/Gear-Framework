using System;

namespace GearFramework.Common.Logger
{
    [Serializable]
    public class DebugSettings : LoggerSettings
    {
        public DebugSettings()
        {
            Enabled = true;
            LogEnabled = true;
            AssertEnabled = true;
            ErrorEnabled = true;
            ExceptionEnabled = true;
            WarningEnabled = true;
        }
    }
}
