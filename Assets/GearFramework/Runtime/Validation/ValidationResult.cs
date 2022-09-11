namespace GearFramework.Runtime.Validation
{
    public class ValidationResult : ValidationError
    {
        public static readonly ValidationResult True = new ValidationResult(null);
        public static readonly ValidationResult StopProcessExecution = new ValidationResult(new ValidationError(ErrorCode.Common_StopProcessExecution));
        
        public bool IsValid { get; }
        public ValidationResult(ValidationError error) : base(error?.Code) => IsValid = error == null;
    }
}
