namespace GearFramework.Runtime.Validation
{
    // reminder: https://github.com/MentorMate/blogposts
    public interface IValidator<T>
    {
        IValidator<T> AddRule(IValidationRule<T> rule);
        ValidationResult Validate(T model);
    }
}
