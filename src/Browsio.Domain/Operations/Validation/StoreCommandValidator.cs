namespace Browsio.Domain
{
    #region << Using >>

    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;
    using Incoding.Quality;
    using JetBrains.Annotations;

    #endregion

    [UsedImplicitly, Obsolete(ObsoleteMessage.ClassNotForDirectUsage, true), ExcludeFromCodeCoverage]
    public class StoreCommandValidator : AbstractValidator<IStoreCommand>
    {
        ////ncrunch: no coverage start
        #region Constructors

        public StoreCommandValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
            RuleFor(r => r.Description).NotEmpty();
            RuleFor(r => r.Category).NotEmpty();
        }

        #endregion

        ////ncrunch: no coverage end
    }
}