using System;

namespace GearFramework.Common
{
    public class TimeBasedNameGenerator : INameGenerator
    {
        public string GetName(string prefix = null, string suffix = null) => $"{((prefix != null) ? prefix : string.Empty)}{DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss-fff")}{((suffix != null) ? suffix : string.Empty)}";
    }
}
