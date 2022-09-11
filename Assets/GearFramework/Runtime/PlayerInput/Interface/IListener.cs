namespace GearFramework.Runtime.PlayerInput
{
    public interface IListener
    {
        event OnKeyPressed OnKeyPressedEvent;
        bool IsActive { get; }
        void Enable();
        void Disable();
    }
}
