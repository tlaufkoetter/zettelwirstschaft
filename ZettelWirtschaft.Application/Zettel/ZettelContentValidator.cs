using FluentValidation;

namespace ZettelWirtschaft.Application.Zettel
{
    public class ZettelContentValidator : AbstractValidator<string>
    {
        public ZettelContentValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().MinimumLength(4).Matches(@"^.*\S((\r?\n)+.*\S)*(\r?\n)*\z");
        }
    }
}