namespace GearFramework.Runtime.Validation
{
    public interface IValidationRule<T>
    {
        ValidationError Error { get; }
        bool Validate(T model);
    }
}
