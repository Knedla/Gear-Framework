using System;
using System.Collections.Generic;
using System.Linq;

namespace GearFramework.Runtime.Validation
{
    public class Validator<T> : IValidator<T>
    {
        private readonly List<Func<T, ValidationError>> validators = new List<Func<T, ValidationError>>();
        public IValidator<T> AddRule(IValidationRule<T> rule)
        {
            validators.Add((T model) => rule.Validate(model) ? null : rule.Error);
            return this;
        }
        public ValidationResult Validate(T model) => new ValidationResult(validators.Select(validate => validate(model)).FirstOrDefault(error => error != null)); // note: returns only one error, regardless of whether there are more than one
    }
}
