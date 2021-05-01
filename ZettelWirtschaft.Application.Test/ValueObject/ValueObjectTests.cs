using System;
using FluentValidation;
using FluentValidation.Results;
using Xunit;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Test.ValueObject
{
    public class ValueObjectTests : AbstractFixture
    {
        private static bool calledValidate;
        private static ValidationResult validationResult;

        override protected void SetUp()
        {
            calledValidate = false;
            validationResult = null;
        }

        override protected void TearDown()
        {
        }

        private class ValueObjectMockValidator<TValue> : AbstractValidator<TValue>
        {
            public override ValidationResult Validate(ValidationContext<TValue> context)
            {
                calledValidate = true;
                if (context.ThrowOnFailures && !validationResult.IsValid)
                {
                    RaiseValidationException(context, validationResult);
                }
                return validationResult;
            }
        }
        private class ValueObjectMock<TValue> : ValueObject<TValue, ValueObjectMockValidator<TValue>>
        {
            internal ValueObjectMock(TValue value) : base(value)
            {
            }
        }

        [Fact]
        public void CallesValidateOnConstruction()
        {
            validationResult = new ValidationResult();
            new ValueObjectMock<int>(0);
            Assert.True(calledValidate);
        }

        [Fact]
        public void ThrowsOnInvalidValue()
        {
            validationResult = new ValidationResult(new[] { new ValidationFailure("Value", "failed") });
            Assert.ThrowsAny<Exception>(() => new ValueObjectMock<int>(0));
        }
    }
}