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
    public class ChangePasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Constructors

        const string MinError = "The password must contain at least 6 characters";

        public ChangePasswordCommandValidator()
        {
            RuleFor(r => r.Password)
                    .NotEmpty().WithMessage("Please enter your new password")
                    .Matches(@".{6,}").WithMessage(MinError);
        }

        #endregion
    }
}