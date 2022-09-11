namespace GearFramework.Runtime.Validation
{
    public class ValidationError
    {
        public string Code { get; }
        public ValidationError(string code) => Code = code;
    }
}
