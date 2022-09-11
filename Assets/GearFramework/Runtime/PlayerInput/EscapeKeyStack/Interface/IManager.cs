namespace GearFramework.Runtime.PlayerInput.EscapeKeyStack
{
    public interface IManager
    {
        void Push(IItem item);
        void Remove(IItem item);
    }
}
