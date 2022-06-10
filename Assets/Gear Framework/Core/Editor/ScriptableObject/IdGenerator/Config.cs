#if UNITY_EDITOR

using System.IO;

namespace UnityEditor
{
    public partial class Config
    {
        public static string UniqueIdGeneratorDirectoryPath => Path.Combine(PathExtension.GetCallerDirectoryPath(), "Generators");
    }
}
#endif
