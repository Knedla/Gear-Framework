namespace GearFramework.Common
{
    public interface INameGenerator
    {
        string GetName(string prefix = null, string suffix = null);
    }
}
