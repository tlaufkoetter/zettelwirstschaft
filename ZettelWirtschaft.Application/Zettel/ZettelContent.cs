using System.Text.RegularExpressions;
using System.Text;
using FluentValidation;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Zettel
{
    public class ZettelContentValidator : AbstractValidator<string>
    {
        public ZettelContentValidator()
        {
            RuleFor(x => x).NotNull().Matches(@"^(.*\S((\r?\n)+.*\S)*(\r?\n)*)?\z");
        }
    }

    public class ZettelContent : ValueObject<string, ZettelContentValidator>
    {
        private static string TrimLineEnds(string value)
        {
            if (value == null)
            {
                return value;
            }

            var sb = new StringBuilder();
            var lines = value.Split('\n');
            foreach (var line in lines)
            {
                sb.AppendLine(line.TrimEnd());
            }

            return Regex.Replace(sb.ToString(), @"\n+\z", "\n");
        }

        public ZettelContent(string value) : base(TrimLineEnds(value))
        {
        }
    }
}