using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Browsio.Domain
{
    using Browsio.Domain.ViewModels;
    using Incoding.CQRS;
    using Incoding.Data;
    using Incoding.Extensions;

    public class GetTEntityByNameQuery<TEntity> : QueryBase<List<TEntity>> where TEntity : class, INameEntity, IEntity
    {
        public GetTEntityByNameQuery() : base() {}

        public GetTEntityByNameQuery(string searchString)
                : base()
        {
            this.SearchString = searchString;
        }

        public string SearchString { get; set; }

        protected override List<TEntity> ExecuteResult()
        {
            return this.Repository.Query(whereSpecification: new TEntityByNameWhereSpec<TEntity>(this.SearchString)).ToList();
        }
    }
}
