using System.IO;
using System.Linq;

namespace GearFramework.Common
{
    public class SettingsUtil
    {
        const string suffixToRemove = "Settings";
        private SettingsUtil() { }

        public static T GetSettings<T>() where T : class, new()
        {
            string path = Path.Combine(PathUtil.PersistentDataDirectoryPath, Config.SettingsData, $"{typeof(T).Namespace.Split('.').Last()}{typeof(T).Name}".Replace(suffixToRemove, string.Empty));
            T settings = DataUtil.LoadDataFromFile_Json<T>(path);

            if (settings == null)
            {
                settings = new T();
                DataUtil.SaveDataToFile_Json(settings, path);
            }

            return settings;
        }
    }
}
