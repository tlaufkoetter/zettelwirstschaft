using System;
using FluentValidation;

namespace ZettelWirtschaft.Engine.ValueObject
{
    public class GuidIdValidator : AbstractValidator<Guid>
    {
        public GuidIdValidator()
        {
            RuleFor(x => x).NotEqual(Guid.Empty);
        }

    }

    public class GuidId : ValueObject<Guid, GuidIdValidator>
    {
        public GuidId(Guid value) : base(value)
        {
        }
    }
}