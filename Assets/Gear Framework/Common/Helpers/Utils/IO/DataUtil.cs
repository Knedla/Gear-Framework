using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace GearFramework.Common
{
    public class DataUtil
    {
        private DataUtil() { }

        public static T LoadDataFromFile_Binary<T>(string path) where T : class
        {
            if (!File.Exists(path))
                return null;

            using (FileStream file = File.Open(path, FileMode.Open))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (T)bf.Deserialize(file);
            }
        }

        public static void SaveDataToFile_Binary<T>(T arg, string path)
        {
            DirectoryUtil.CreateDirectory(Path.GetDirectoryName(path));

            using (FileStream file = File.Create(path))
            {
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(file, arg);
            }
        }

        public static T LoadDataFromFile_Json<T>(string path) where T : class
        {
            if (File.Exists(path))
                return JsonUtility.FromJson<T>(File.ReadAllText(path));

            return null;
        }

        public static void SaveDataToFile_Json<T>(T arg, string path)
        {
            DirectoryUtil.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(arg, true));
        }
    }
}
