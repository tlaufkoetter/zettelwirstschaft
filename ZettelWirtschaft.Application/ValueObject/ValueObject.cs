using FluentValidation;
using FluentValidation.Results;

namespace ZettelWirtschaft.Application.ValueObject
{
    public class ValueObject<TValue, TValidator> where TValidator : AbstractValidator<TValue>, new()
    {
        public TValue Value { get; }

        protected ValueObject(TValue value)
        {
            GetValidator().ValidateAndThrow(value);
            Value = value;
        }

        public static implicit operator TValue(ValueObject<TValue, TValidator> obj)
        {
            return obj.Value;
        }

        public static TValidator GetValidator()
        {
            return new TValidator();
        }
    }
}