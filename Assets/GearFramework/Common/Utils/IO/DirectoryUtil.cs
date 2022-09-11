using System.IO;

namespace GearFramework.Common
{
    public class DirectoryUtil
    {
        private DirectoryUtil() { }
        
        public static void CreateDirectory(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }
    }
}
