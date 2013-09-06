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
    public class GetProductByAmazonQueryValidator : AbstractValidator<GetProductByAmazonQuery>
    {
        #region Constructors

        public GetProductByAmazonQueryValidator()
        {
            RuleFor(r => r.Title).NotEmpty();
        }

        #endregion
    }
}