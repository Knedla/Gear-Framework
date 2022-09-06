namespace GearFramework.LogRecorder
{
    public interface IManager
    {
        string DirectoryPath { get; }
        void Log();
    }
}
