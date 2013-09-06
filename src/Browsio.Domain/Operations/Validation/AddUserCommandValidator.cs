namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    [UsedImplicitly, Obsolete(ObsoleteMessage.NotSupportForThisImplement, true), ExcludeFromCodeCoverage]
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        #region Constructors

        const string MinError = "The password must contain at least 6 characters";

        public AddUserCommandValidator()
        {
            RuleFor(r => r.Login)
                    .NotEmpty()
                    .EmailAddress();
            RuleFor(r => r.Password)
                    .NotEmpty().Matches(@".{6,}").WithMessage(MinError);
            RuleFor(r => r.RetryPassword)
                    .Equal(r => r.Password);
        }

        #endregion
    }
}