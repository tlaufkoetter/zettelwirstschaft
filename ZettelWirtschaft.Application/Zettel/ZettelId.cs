using System.Text.RegularExpressions;
using System;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using System.Threading;

namespace ZettelWirtschaft.Application.Zettel
{
    public class ZettelId
    {
        private class ZettelIdValidator : AbstractValidator<ZettelId>
        {
            private const string Pattern = @"^[a-zA-Z0-9]+((-[a-zA-Z0-9]+)|(_[a-zA-Z0-9]+))*$";
            public ZettelIdValidator()
            {
                RuleFor(x => x.Id).NotNull().NotEmpty().Matches(Pattern);
            }
        }
        public string Id { get; }

        public ZettelId(string id)
        {
            this.Id = id;
            new ZettelIdValidator().ValidateAndThrow(this);
        }

        public static implicit operator string(ZettelId id)
        {
            return id.Id;
        }
    }
}