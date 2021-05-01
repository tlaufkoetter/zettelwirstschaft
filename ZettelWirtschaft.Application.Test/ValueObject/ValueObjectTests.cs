using System;
using FluentValidation;
using FluentValidation.Results;
using Xunit;
using ZettelWirtschaft.Application.ValueObject;

namespace ZettelWirtschaft.Application.Test.ValueObject
{
    public class ValueObjectTests : IDisposable
    {
        private static bool calledValidate;
        private static ValidationResult validationResult;

        private void SetUp()
        {
            calledValidate = false;
            validationResult = null;
        }

        private void TearDown()
        {
        }

        #region Dispose
        private bool disposedValue;

        public void CallsValidationInConstructorTest()
        {
            SetUp();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    TearDown();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~ValueObjectTests()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

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