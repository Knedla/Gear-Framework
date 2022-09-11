using System;
using System.Collections.Generic;

namespace GearFramework.Runtime.LogRecorder
{
    [Serializable]
    public class Logs
    {
        public List<Log> Items;

        public Logs() => Items = new List<Log>();
    }
}
