using FluentValidation;

namespace ZettelWirtschaft.Application.ValueObject
{
    public class ValueObject<TValue, TValidator> where TValidator : AbstractValidator<TValue>, new()
    {
        public TValue Value { get; }

        protected ValueObject(TValue value)
        {
            Validate(value);
            Value = value;
        }

        private void Validate(TValue obj)
        {
            new TValidator().ValidateAndThrow(obj);
        }

        public static implicit operator TValue(ValueObject<TValue, TValidator> obj)
        {
            return obj.Value;
        }
    }
}