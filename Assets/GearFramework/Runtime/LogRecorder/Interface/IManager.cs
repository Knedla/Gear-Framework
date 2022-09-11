namespace GearFramework.Runtime.LogRecorder
{
    public interface IManager
    {
        string DirectoryPath { get; }
        void Log();
    }
}
