namespace Browsio.Domain
{
    using System;
    using System.Linq.Expressions;
    using Incoding;

    public class StoreByGenreOptWhereSpec : Specification<Store>
    {
        readonly string genreId;

        public StoreByGenreOptWhereSpec(string genreId)
        {
            this.genreId = genreId;
        }

        public override Expression<Func<Store, bool>> IsSatisfiedBy()
        {
            if (string.IsNullOrWhiteSpace(this.genreId))
                return null;

            return store => store.Genre.Id == this.genreId;
        }
    }
}