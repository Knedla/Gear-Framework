using System;

namespace GearFramework.LogRecorder
{
    [Serializable]
    public class Settings
    {
        public bool Enabled;
        public bool LogOnPause;
        public bool LogOnFocusLost;
        public string LogDirectoryName;

        public Settings()
        {
#if UNITY_EDITOR
            Enabled = true;
#else
            Enabled = false;
#endif
            LogOnPause = true;
            LogOnFocusLost = true;
            LogDirectoryName = "Log";
        }

    }
}
