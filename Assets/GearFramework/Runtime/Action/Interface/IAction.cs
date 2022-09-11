using GearFramework.Runtime.Validation;

namespace GearFramework.Runtime.Action
{
    public interface IAction // thoughts: whether the action should have status (executed, rolled back)
    {
        ValidationResult Validate();
        void Execute();
        void Rollback();
    }
}
