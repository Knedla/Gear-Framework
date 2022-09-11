using UnityEngine;

namespace GearFramework.Common
{
    public class PathUtil
    {
        private PathUtil() { }
        public static string PersistentDataDirectoryPath => Application.persistentDataPath;
    }
}
