using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Browsio.Domain
{
    using Incoding;
    using Incoding.CQRS;
    using Incoding.Extensions;

    public class SearchStoreQuery : QueryBase<List<SearchStoreQuery.Response>>
    {
        public string GenreId { get; set; }

        public CategoryOfType? Category { get; set; }

        public string[] StoreIds { get; set; }

        protected override List<Response> ExecuteResult()
        {
            Specification<Store> whereSpecification = new StoreByCategoryOptWhereSpec(this.Category)
                    .And(new StoreByGenreOptWhereSpec(this.GenreId))
                    .And(new StoreByIdsOptWhereSpec(StoreIds));

            return this.Repository.Query(whereSpecification: whereSpecification,
                                    fetchSpecification: new StoreWithUserFetchSpec())
                             .ToList()
                             .Select(r => new Response
                                              {
                                                  Id = r.Id.ToString(),
                                                  Name = r.Name,
                                                  UserId = r.User.Id.ToString(),
                                                  User = r.User.FullName,
                                                  CategoryAsClass = r.CategoryAsClass
                                              })
                             .ToList();
        }

        public class Response
        {
            #region Properties

            public string Id { get; set; }

            public string Name { get; set; }

            public string User { get; set; }

            public string UserId { get; set; }

            public string CategoryAsClass { get; set; }

            #endregion
        }

    }

}
