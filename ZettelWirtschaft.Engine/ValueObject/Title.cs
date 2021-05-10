using FluentValidation;

namespace ZettelWirtschaft.Engine.ValueObject
{
    public class TitleValidator : AbstractValidator<string>
    {
        public TitleValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().MinimumLength(4).MaximumLength(100).Matches(@"^\S+(\ +\S+)*\z");
        }
    }

    public class Title : ValueObject<string, TitleValidator>
    {
        public Title(string value) : base(value.Trim())
        {
        }
    }
}
