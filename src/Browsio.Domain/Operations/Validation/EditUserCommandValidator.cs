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
    public class EditUserCommandValidator : AbstractValidator<EditUserCommand>
    {
        #region Constructors

        public EditUserCommandValidator()
        {
            RuleFor(r => r.FirstName)
                    .NotEmpty();
            RuleFor(r => r.LastName)
                    .NotEmpty();
        }

        #endregion
    }
}