namespace GearFramework.Runtime.PlayerInput.EscapeKeyStack
{
    public interface IItem
    {
        bool BlockEscapeKey { get; }
        void EscapeKeyPressed();
    }
}
