using FluentValidation;

namespace ZettelWirtschaft.Engine.ValueObject
{
    public class StringIdValidator : AbstractValidator<string>
    {
        private const string Pattern = @"^[a-zA-Z0-9]+((-[a-zA-Z0-9]+)|(_[a-zA-Z0-9]+))*$";
        public StringIdValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().Matches(Pattern);
        }
    }

    public class StringId : ValueObject<string, StringIdValidator>
    {
        public StringId(string value) : base(value)
        {
        }
    }
}