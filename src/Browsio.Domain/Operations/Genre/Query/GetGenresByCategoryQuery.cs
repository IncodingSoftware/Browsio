namespace Browsio.Domain
{
    #region << Using >>

    using System.Collections.Generic;
    using System.Linq;
    using Incoding.CQRS;

    #endregion

    public class GetGenresByCategoryQuery : QueryBase<List<Genre>>
    {
        #region Properties

        public CategoryOfType? Category { get; set; }

        #endregion

        protected override List<Genre> ExecuteResult()
        {
            return Repository.Query(whereSpecification: new GenreByCategoryOptWhereSpec(Category))
                             .ToList();
        }
    }
}