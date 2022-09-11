using GearFramework.Common;
using System;
using System.IO;
using UnityEngine;

namespace GearFramework.Runtime.LogRecorder
{
    public class Manager : IManager
    {
        public string DirectoryPath => Path.Combine(PathUtil.PersistentDataDirectoryPath, Settings.LogDirectoryName);
        public Settings Settings { get; }

        INameGenerator nameGenerator;
        
        Logs logs;

        public Manager(Settings settings, INameGenerator nameGenerator)
        {
            Settings = settings;

            if (!Application.isPlaying) // note: if there is no this line of code, the constructor can be called in editor mode and with it create and attach the controller component to the open scene
                return;

            this.nameGenerator = nameGenerator;

            logs = new Logs();

            Controller logController = new GameObject(typeof(Controller).Name).AddComponent<Controller>();
            logController.SetData(this);

            Application.logMessageReceived += Application_logMessageReceived;
        }

        void Application_logMessageReceived(string condition, string stackTrace, LogType type)
        {
            if (Settings.Enabled)
                logs.Items.Add(new Log(condition, stackTrace, type, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")));
        }

        public void Log()
        {
            if (!Settings.Enabled || logs.Items.Count == 0)
                return;

            DataUtil.SaveDataToFile_Json(logs, Path.Combine(DirectoryPath, nameGenerator.GetName(suffix: ".txt")));
            logs.Items.Clear();
        }
    }
}
